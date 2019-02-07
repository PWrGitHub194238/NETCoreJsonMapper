using NETCoreJsonMapper.Commons.Mappings;
using NETCoreJsonMapper.Commons.Properties;
using NETCoreJsonMapper.Loggers.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NETCoreJsonMapper.Commons.Utils
{
    internal class ReflectionUtils
    {
        public static bool IsDefault(object value, Type type)
        {
            return value == null
                || (value.GetType().IsValueType
                    ? value.Equals(Activator.CreateInstance(type))
                    : value == null);
        }

        internal static ISet<string> GetClassPropertyCollection(ISet<string> classKeySet,
                    Type jsonType, bool innerProperties)
        {
            classKeySet.UnionWith(GetJsonProperties(jsonClassType: jsonType)
                .Select(p => p.Name).ToHashSet());

            if (innerProperties)
            {
                foreach (Type innerType in GetClassInnerClassCollection(jsonType: jsonType))
                {
                    classKeySet = GetClassPropertyCollection(classKeySet: classKeySet,
                        jsonType: innerType, innerProperties: innerProperties);
                }
            }
            return classKeySet;
        }

        internal static ISet<string> GetClassPropertyOnlyCollection(Type jsonType)
        {
            return GetClassPropertyCollection(classKeySet: new HashSet<string>(),
                jsonType: jsonType, innerProperties: false);
        }

        /// <summary>
        /// Checks whether a given jsonString can be deserialized onto a given expectedType.
        /// </summary>
        /// <param name="jsonString">Well formatted JSON string.</param>
        /// <param name="expectedType">The expected type of the giver JSON string deserialization's result.
        /// </param>
        internal static bool ValidateJsonStringType(string jsonString, Type expectedType)
        {
            DefaultLogger.Verbose(Resources.LOG_VERBOSE_VALIDATE_JSON_STRING, expectedType.FullName);
            ISet<string> jsonKeySet = JsonUtils.GetJsonKeyCollection(jsonString: jsonString);
            DefaultLogger.Verbose(Resources.LOG_VERBOSE_VALIDATE_JSON_STRING_KEY_SET, jsonKeySet);
            ISet<string> classKeySet = GetClassPropertyCollection(jsonType: expectedType);
            DefaultLogger.Verbose(Resources.LOG_VERBOSE_VALIDATE_JSON_STRING_CLASS_PROPERTY_SET, expectedType.FullName, classKeySet);
            return JsonUtils.ValidateJsonWithClass(sourceJsonKeySet: jsonKeySet,
                targetClassKeySet: classKeySet);
        }

        private static IEnumerable<Type> GetClassInnerClassCollection(Type jsonType)
        {
            return jsonType.GetNestedTypes(bindingAttr: BindingFlags.Instance | BindingFlags.Public)
                .Where(t => IsObjectJsonField(type: t));
        }

        private static ISet<string> GetClassPropertyCollection(Type jsonType)
        {
            return GetClassPropertyCollection(classKeySet: new HashSet<string>(),
                jsonType: jsonType, innerProperties: true);
        }

        private static IEnumerable<PropertyInfo> GetJsonProperties(Type jsonClassType)
        {
            return jsonClassType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => IsPropertyJsonField(property: p));
        }

        private static bool IsObjectJsonField(Type type)
        {
            return Attribute.IsDefined(element: type,
                attributeType: typeof(JsonObjectAttribute));
        }

        private static bool IsPropertyJsonField(PropertyInfo property)
        {
            return Attribute.IsDefined(element: property,
                attributeType: typeof(JsonPropertyAttribute));
        }

        internal static void InvokeSetEmptyProperties(object sourceInstance, object targetInstance)
        {
            Type sourceType = sourceInstance.GetType();
            Type targetType = targetInstance.GetType();
            Type sourcePropertyMapperType = typeof(SourcePropertyMapper<,>).MakeGenericType(sourceType, targetType);
            MethodInfo setEmptyPropertiesMethodInfo = sourcePropertyMapperType.GetMethod(name: "SetEmptyProperties",
                bindingAttr: BindingFlags.Instance | BindingFlags.NonPublic,
                binder: Type.DefaultBinder, types: new Type[] { targetType },
                modifiers: null);
            setEmptyPropertiesMethodInfo.Invoke(
                Activator.CreateInstance(type: sourcePropertyMapperType, args: sourceInstance),
                new object[] { targetInstance });
        }
    }
}