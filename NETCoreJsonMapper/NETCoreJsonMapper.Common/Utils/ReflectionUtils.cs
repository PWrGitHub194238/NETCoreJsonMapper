using NETCoreJsonMapper.Common.Mappings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NETCoreJsonMapper.Common.Utils
{
    internal class ReflectionUtils
    {
        /// <summary>
        /// Checks whether a given jsonString can be deserialized onto a given expectedType.
        /// </summary>
        /// <param name="jsonString">Well formatted JSON string.</param>
        /// <param name="expectedType">The expected type of the giver JSON string deserialization's result.
        /// </param>
        /// <returns></returns>
        internal static bool ValidateJsonStringType(string jsonString, Type expectedType)
        {
            ISet<string> jsonKeySet = JsonUtils.GetJsonKeyCollection(jsonString: jsonString);
            ISet<string> classKeySet = GetClassPropertyCollection(jsonType: expectedType);
            return JsonUtils.ValidateJsonWithClass(sourceJsonKeySet: jsonKeySet,
                targetClassKeySet: classKeySet);
        }

        internal static void SetEmptyProperties<TJsonTarget>(AJsonDataSource<TJsonTarget> sourceInstance,
            TJsonTarget targetInstance, Type sourceType, Type targetType) where TJsonTarget : new()
        {
            ISet<string> classKeySet = GetClassPropertyOnlyCollection(sourceType);
            foreach (string property in classKeySet)
            {
                PropertyInfo sourceProperty = sourceType.GetProperty(property,
                    BindingFlags.Instance | BindingFlags.Public);

                if (sourceProperty != null && sourceProperty.GetMethod != null)
                {
                    PropertyInfo targetProperty = targetType.GetProperty(property,
                        BindingFlags.Instance | BindingFlags.Public);
                    if (targetProperty != null && targetProperty.GetMethod != null)
                    {
                        object sourcePropertyValue = sourceProperty.GetValue(sourceInstance);
                        Type sourcePropertyType = sourceProperty.PropertyType;
                        object targetPropertyValue = targetProperty.GetValue(targetInstance);
                        Type targetPropertyType = targetProperty.PropertyType;
                        if ((targetPropertyValue == null || targetPropertyValue.Equals(0)) && sourcePropertyType == targetPropertyType)
                        {
                            targetProperty.SetValue(targetInstance, sourcePropertyValue);
                        }
                    }
                }
            }
        }

        internal static ISet<string> GetClassPropertyCollection(Type jsonType)
        {
            return GetClassPropertyCollection(classKeySet: new HashSet<string>(),
                jsonType: jsonType, innerProperties: true);
        }

        internal static ISet<string> GetClassPropertyOnlyCollection(Type jsonType)
        {
            return GetClassPropertyCollection(classKeySet: new HashSet<string>(),
                jsonType: jsonType, innerProperties: false);
        }

        internal static ISet<string> GetClassPropertyCollection(ISet<string> classKeySet,
            Type jsonType, bool innerProperties)
        {
            classKeySet.UnionWith(jsonType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => IsPropertyJsonField(property: p))
                .Select(p => p.Name).ToHashSet());

            if (innerProperties)
            {
                foreach (Type innerType in GetClassInnerClassCollection(jsonType: jsonType))
                {
                    classKeySet.UnionWith(GetClassPropertyCollection(classKeySet: classKeySet,
                        jsonType: innerType, innerProperties: innerProperties));
                }
            }
            return classKeySet;
        }

        private static IEnumerable<Type> GetClassInnerClassCollection(Type jsonType)
        {
            return jsonType.GetNestedTypes(BindingFlags.Instance | BindingFlags.Public)
                .Where(t => IsTypeJsonField(type: t));
        }

        private static bool IsPropertyJsonField(PropertyInfo property)
        {
            return Attribute.IsDefined(property, typeof(JsonPropertyAttribute));
        }

        private static bool IsTypeJsonField(Type type)
        {
            return Attribute.IsDefined(type, typeof(JsonObjectAttribute));
        }
    }
}