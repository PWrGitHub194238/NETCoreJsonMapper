using NETCoreJsonMapper.Interface.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCoreJsonMapper.Utils
{
    internal static class TypeUtils
    {
        public static readonly Type IJsonDataTargetType = typeof(IJsonDataTarget);
        public static readonly Type GenericIJsonDataSourceType = typeof(IJsonDataSource<>);

        internal static bool IsAssignableFromIJsonDataTarget(Type type)
        {
            return IJsonDataTargetType.IsAssignableFrom(type);
        }
        internal static bool IsAssignableFromGenericIJsonDataSource(Type type, Type innerType)
        {
            return ReflectionUtils.MakeGenericIJsonDataSource(innerType: innerType)
                .IsAssignableFrom(type);
        }

        internal static bool EqualsDefault<T>(T type)
        {
            return type == null || type.Equals(default(T));
        }

        internal static bool IsInstanceable(Type type)
        {
            bool result = true;
            result &= type.IsClass;
            result &= !type.IsInterface;
            result &= !type.IsAbstract;
            return result;
        }
    }
}
