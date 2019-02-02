using NETCoreJsonMapper.Common.Mappings;
using NETCoreJsonMapper.Interface.Mappings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

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
            TJsonTarget targetInstance, Type sourceType, Type targetType)
            where TJsonTarget : IJsonDataTarget, new() => SetEmptyProperties(
                sourceInstance: sourceInstance, targetInstance: (object)targetInstance,
                sourceType: sourceType, targetType: targetType);

        private static void SetEmptyProperties<TJsonTarget>(AJsonDataSource<TJsonTarget> sourceInstance,
            object targetInstance, Type sourceType, Type targetType)
        where TJsonTarget : IJsonDataTarget, new()
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

                        if (sourcePropertyType.IsValueType)
                        {
                            if (targetPropertyType.IsValueType)
                            {
                                if (targetPropertyValue.Equals(Activator.CreateInstance(targetPropertyType)))
                                {
                                    targetProperty.SetValue(targetInstance, sourcePropertyValue);
                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                if (targetPropertyValue == null)
                                {
                                    if (targetPropertyType.Equals(typeof(string)))
                                    {
                                        targetProperty.SetValue(targetInstance, sourcePropertyValue.ToString());
                                    }
                                    else
                                    {
                                        var constructorInfo = targetPropertyType.GetConstructor(new Type[] { sourcePropertyType });

                                        if (constructorInfo == null)
                                        {
                                            constructorInfo = targetPropertyType.GetConstructor(Type.EmptyTypes);
                                            targetProperty.SetValue(targetInstance, constructorInfo.Invoke(new object[] { }));
                                        }
                                        else
                                        {
                                            targetProperty.SetValue(targetInstance, constructorInfo.Invoke(new object[] { sourcePropertyValue }));
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (targetPropertyType.IsValueType)
                            {
                                if (targetPropertyValue.Equals(Activator.CreateInstance(targetPropertyType)))
                                {
                                    if (targetPropertyType.Equals(typeof(string)))
                                    {
                                        targetProperty.SetValue(targetInstance, sourcePropertyValue.ToString());
                                    }
                                    else
                                    {
                                        try
                                        {
                                            targetProperty.SetValue(targetInstance, sourcePropertyValue);
                                        }
                                        catch (Exception e)
                                        {

                                            throw e;
                                        }
                                    };
                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                if (targetPropertyValue == null)
                                {
                                    if (targetPropertyType.Equals(typeof(string)))
                                    {
                                        targetProperty.SetValue(targetInstance, sourcePropertyValue.ToString());
                                    }
                                    else
                                    {
                                        var members = targetInstance.GetType().Assembly.GetTypes().SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public))

                                            .Where(m =>
                                            {
                                                return Attribute.IsDefined(element: m, attributeType: typeof(ExtensionAttribute))
                                                && m.GetParameters().First().ParameterType.Equals(targetPropertyType)
                                                && m.GetParameters().Skip(1).First().ParameterType.Equals(sourcePropertyType);
                                            }).FirstOrDefault();

                                        if (members != null)
                                        {
                                            var obj = Activator.CreateInstance(targetPropertyType);
                                            members.Invoke(null, new object[] { obj, sourcePropertyValue });
                                            targetProperty.SetValue(targetInstance, obj);
                                        }
                                        else
                                        {


                                            object[] objArgs = new object[] { };
                                            var constructorInfo = targetPropertyType.GetConstructor(new Type[] { sourcePropertyType });

                                            if (constructorInfo != null)
                                            {
                                                objArgs = new object[] { sourcePropertyValue };
                                            }

                                            var obj = Activator.CreateInstance(targetPropertyType, objArgs);

                                            targetProperty.SetValue(targetInstance, obj);
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }

        internal static ISet<string> GetClassPropertyCollection(Type jsonType) => GetClassPropertyCollection(
        classKeySet: new HashSet<string>(), jsonType: jsonType, innerProperties: true);

        internal static ISet<string> GetClassPropertyOnlyCollection(Type jsonType) => GetClassPropertyCollection(
            classKeySet: new HashSet<string>(), jsonType: jsonType, innerProperties: false);

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

        private static IEnumerable<Type> GetClassInnerClassCollection(Type jsonType) => jsonType.GetNestedTypes(
                BindingFlags.Instance | BindingFlags.Public)
            .Where(t => IsTypeJsonField(type: t));

        private static bool IsPropertyJsonField(PropertyInfo property) => Attribute.IsDefined(
            element: property, attributeType: typeof(JsonPropertyAttribute));

        private static bool IsTypeJsonField(Type type) => Attribute.IsDefined(
            element: type, attributeType: typeof(JsonObjectAttribute));
    }
}