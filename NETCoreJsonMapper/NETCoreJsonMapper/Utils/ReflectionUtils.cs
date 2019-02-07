using System;
using System.IO;
using System.Reflection;

namespace NETCoreJsonMapper.Utils
{
    public static class ReflectionUtils
    {
        internal static string AssemblyDirectory {
            get {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        internal static Type MakeGenericIJsonDataSource(Type innerType)
        {
            return TypeUtils.GenericIJsonDataSourceType
                .MakeGenericType(innerType);
        }
    }
}