using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace NETCoreJsonMapper.Utils
{
    public static class ReflectionUtils
    {
        private const string DLL_FILE_SEARCH_PATTERN = "*.dll";

        private static readonly AssemblyLoadContext assemblyLoadContext = AssemblyLoadContext.Default;

        internal static string AssemblyDirectory {
            get {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private static IEnumerable<string> GetSolutionAssemblyPaths()
        {
            return Directory.EnumerateFiles(AssemblyDirectory,
                DLL_FILE_SEARCH_PATTERN, SearchOption.TopDirectoryOnly);
        }

        private static IEnumerable<Assembly> LoadSolutionAssemblies()
        {
            foreach (string assemblyFullPath in GetSolutionAssemblyPaths())
            {
                yield return assemblyLoadContext.LoadFromAssemblyPath(assemblyPath: assemblyFullPath);
            }
        }

        internal static IEnumerable<Type> GetIJsonDataTargetTypes()
        {
            foreach (Assembly assembly in LoadSolutionAssemblies())
            {
                Type jsonDataTargetType = GetJsonDataTargetFromAssemblyOrDefault(assembly: assembly);
                if (!TypeUtils.EqualsDefault(type: jsonDataTargetType))
                {
                    yield return jsonDataTargetType;
                }
            }
        }

        internal static Type MakeGenericIJsonDataSource(Type innerType)
        {
            return TypeUtils.GenericIJsonDataSourceType
                .MakeGenericType(innerType);
        }

        internal static IEnumerable<Type> GetGenericIJsonDataSourceTypes(Type innerType)
        {
            foreach (Assembly assembly in LoadSolutionAssemblies())
            {
                Type genericJsonDataSourceType = GetGenericIJsonDataSourceFromAssemblyOrDefault(
                    assembly: assembly, innerType: innerType);
                if (!TypeUtils.EqualsDefault(type: genericJsonDataSourceType))
                {
                    yield return genericJsonDataSourceType;
                }
            }
        }

        private static Type GetGenericIJsonDataSourceFromAssemblyOrDefault(Assembly assembly, Type innerType)
        {
            return assembly.GetTypes()
                    .Where(t => IsTypeInheritsFromGenericJsonDataSourceInterface(type: t, innerType: innerType))
                    .FirstOrDefault();
        }

        private static bool IsTypeInheritsFromGenericJsonDataSourceInterface(Type type, Type innerType)
        {
            bool result = true;
            result &= TypeUtils.IsAssignableFromGenericIJsonDataSource(
                type: type, innerType: innerType);
            result &= TypeUtils.IsInstanceable(type: type);
            return result;
        }

        private static Type GetJsonDataTargetFromAssemblyOrDefault(Assembly assembly)
        {
            return assembly.GetTypes()
                    .Where(IsTypeInheritsFromJsonDataTargetInterface)
                    .FirstOrDefault();
        }

        private static bool IsTypeInheritsFromJsonDataTargetInterface(Type type)
        {
            bool result = true;
            result &= TypeUtils.IsAssignableFromIJsonDataTarget(type: type);
            result &= TypeUtils.IsInstanceable(type: type);
            return result;
        }
    }
}