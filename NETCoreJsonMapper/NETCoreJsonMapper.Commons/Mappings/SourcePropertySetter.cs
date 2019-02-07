using NETCoreJsonMapper.Commons.Enums;
using NETCoreJsonMapper.Commons.Utils;
using NETCoreJsonMapper.Extensions.System;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NETCoreJsonMapper.Commons.Mappings
{
    internal class SourcePropertySetter<TJsonSource, TJsonTarget>
    {
        private readonly PropertyInfo sourceProperty;
        private readonly Type sourcePropertyType;
        private readonly PropertyInfo targetProperty;
        private readonly Type targetPropertyType;
        private TJsonSource dataSourceInstance;
        private object sourcePropertyValue;
        private TJsonTarget targetInstance;
        private object targetPropertyValue;

        public SourcePropertySetter(TJsonSource dataSourceInstance,
            PropertyInfo sourceProperty, TJsonTarget targetInstance, PropertyInfo targetProperty)
        {
            this.dataSourceInstance = dataSourceInstance;
            this.targetInstance = targetInstance;
            this.sourceProperty = sourceProperty;
            this.targetProperty = targetProperty;

            sourcePropertyType = sourceProperty.PropertyType;
            targetPropertyType = targetProperty.PropertyType;
            sourcePropertyValue = sourceProperty.GetValue(dataSourceInstance);
            targetPropertyValue = targetProperty.GetValue(targetInstance);
        }

        public PropertyMapType GetPropertyMapType(Type sourceType, Type targetType) =>
            sourceType.IsValueType
                ? targetType.IsValueType
                    ? PropertyMapType.VALUE_TO_VALUE
                    : PropertyMapType.VALUE_TO_CLASS
                : targetType.IsValueType
                    ? PropertyMapType.CLASS_TO_VALUE
                    : PropertyMapType.CLASS_TO_CLASS;

        public bool HasConverterExtensionMethod(object sourceInstance, Type targetType, out MethodInfo resultMethod)
        {
            resultMethod = GetExtensionMethods(instance: sourceInstance, extendedType: sourcePropertyType, convertToType: targetType);

            return resultMethod != null;
        }

        public void SetEmptyProperty()
        {
            switch (GetPropertyMapType(sourceType: sourcePropertyType, targetType: targetPropertyType))
            {
                case PropertyMapType.VALUE_TO_VALUE:
                    SetEmptyPropertyValueToValue();
                    break;

                case PropertyMapType.VALUE_TO_CLASS:
                    SetEmptyPropertyValueToClass();
                    break;

                case PropertyMapType.CLASS_TO_VALUE:
                    SetEmptyPropertyClassToValue();
                    break;

                case PropertyMapType.CLASS_TO_CLASS:
                    SetEmptyPropertyClassToClass();
                    break;

                default:
                    break;
            }
        }

        private MethodInfo GetExtensionMethod(Type[] types, Type extendedType, Type convertToType)
        {
            return types.SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public))
                .Where(m => IsConverterExtensionMethod(method: m, extendedType: extendedType, convertToType: convertToType))
                .FirstOrDefault();
        }

        private MethodInfo GetExtensionMethods(object instance, Type extendedType, Type convertToType)
        {
            return GetExtensionMethod(types: instance.GetType().Assembly.GetTypes(),
                extendedType: extendedType, convertToType: convertToType);
        }

        private bool IsConverterExtensionMethod(MethodInfo method, Type extendedType, Type convertToType)
        {
            ParameterInfo[] parameters = method.GetParameters();
            return parameters.Count().Equals(2)
                && Attribute.IsDefined(element: method, attributeType: typeof(ExtensionAttribute))
                && parameters.First().ParameterType.Equals(extendedType)
                && parameters.Skip(1).First().ParameterType.Equals(convertToType);
        }

        private void SetEmptyPropertyByConverterExtensionMethod(MethodInfo conversionMethod)
        {
            object obj = Activator.CreateInstance(targetPropertyType);
            conversionMethod.Invoke(null, new object[] { sourcePropertyValue, obj });
            targetProperty.SetValue(targetInstance, obj);
        }

        private void SetEmptyPropertyClassToClass()
        {
            if (ReflectionUtils.IsDefault(value: targetPropertyValue, type: targetPropertyType))
            {
                if (targetPropertyType.IsStringType())
                {
                    targetProperty.SetValue(targetInstance,
                        sourcePropertyValue.ToString());
                }
                else if (HasConverterExtensionMethod(sourceInstance: dataSourceInstance,
                    targetType: targetPropertyType, resultMethod: out MethodInfo convertMethodInfo))
                {
                    SetEmptyPropertyByConverterExtensionMethod(conversionMethod: convertMethodInfo);
                }
                else if (targetPropertyType.HasCloneConstructor(cloneToParameterType: sourcePropertyType))
                {
                    targetProperty.SetValue(targetInstance,
                        Activator.CreateInstance(targetPropertyType, sourcePropertyValue));
                }
                else if (sourcePropertyType.IsCollection() && targetPropertyType.IsCollection())
                {
                    SetEmptyPropertyListToList();
                }
                else if (targetPropertyType.IsAssignableFrom(sourcePropertyType))
                {
                    targetProperty.SetValue(targetInstance, sourcePropertyValue);
                }
                else
                {
                    targetProperty.SetValue(targetInstance,
                        Activator.CreateInstance(type: targetPropertyType));
                }
            }
        }

        private void SetEmptyPropertyClassToValue()
        {
            if (ReflectionUtils.IsDefault(value: targetPropertyValue, type: targetPropertyType))
            {
                try
                {
                    targetProperty.SetValue(targetInstance, sourcePropertyValue);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
            }
        }

        private void SetEmptyPropertyListToList()
        {
            // Create list by default constructor
            targetProperty.SetValue(targetInstance,
                Activator.CreateInstance(targetPropertyType));

            foreach (object sourceListElement in (IEnumerable)sourceProperty.GetValue(dataSourceInstance, null))
            {
                object targetListElement = Activator.CreateInstance(
                    targetPropertyType.GenericTypeArguments.First(),
                    sourceListElement);
                targetPropertyType.GetMethod("Add")
                    .Invoke(targetProperty.GetValue(targetInstance),
                        new[] { targetListElement });
                ReflectionUtils.InvokeSetEmptyProperties(sourceInstance: sourceListElement,
                    targetInstance: targetListElement);
            }
        }

        private void SetEmptyPropertyValueToClass()
        {
            if (ReflectionUtils.IsDefault(value: targetPropertyValue, type: targetPropertyType))
            {
                if (targetPropertyType.IsStringType())
                {
                    targetProperty.SetValue(targetInstance,
                        sourcePropertyValue.ToString());
                }
                else if (targetPropertyType.HasCloneConstructor(cloneToParameterType: sourcePropertyType))
                {
                    targetProperty.SetValue(targetInstance,
                        Activator.CreateInstance(targetPropertyType, sourcePropertyValue));
                }
                else
                {
                    targetProperty.SetValue(targetInstance,
                        Activator.CreateInstance(type: targetPropertyType));
                }
            }
        }

        private void SetEmptyPropertyValueToValue()
        {
            if (ReflectionUtils.IsDefault(value: targetPropertyValue, type: targetPropertyType))
            {
                targetProperty.SetValue(targetInstance, sourcePropertyValue);
            }
            else
            {
            }
        }
    }
}