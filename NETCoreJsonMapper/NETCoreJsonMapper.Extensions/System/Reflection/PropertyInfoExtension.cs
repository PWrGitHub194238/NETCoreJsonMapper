﻿using System.Reflection;

namespace NETCoreJsonMapper.Extension.System.Reflection
{
    public static class PropertyInfoExtension
    {
        public static bool HasGetter(this PropertyInfo property)
        {
            return property != null
                && property.GetMethod != null;
        }
    }
}