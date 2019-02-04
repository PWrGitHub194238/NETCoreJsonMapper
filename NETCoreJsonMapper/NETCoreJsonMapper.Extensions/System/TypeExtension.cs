using NETCoreJsonMapper.Extension.System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NETCoreJsonMapper.Extension.System
{
    public static class TypeExtension
    {
        public static PropertyInfo GetPublicInstanceProperty(this Type type, string propertyName)
        {
            return type.GetProperty(
                name: propertyName,
                bindingAttr: BindingFlags.Instance | BindingFlags.Public);
        }

        public static bool HasCloneConstructor(this Type classType, Type cloneToParameterType)
        {
            return classType.GetConstructors()
                .Any(c => c.HasMethodOneParameterOfType(parameterType: cloneToParameterType));
        }

        public static bool IsCollection(this Type type)
        {
            bool result = type.IsGenericType;
            result &= type.GenericTypeArguments.Count() == 1;
            if (result)
            {
                Type t = typeof(ICollection<>).MakeGenericType(type.GenericTypeArguments);
                result &= t.IsAssignableFrom(type);
            }
            return result;
        }

        public static bool IsStringType(this Type type)
        {
            return type.Equals(typeof(string));
        }
    }
}