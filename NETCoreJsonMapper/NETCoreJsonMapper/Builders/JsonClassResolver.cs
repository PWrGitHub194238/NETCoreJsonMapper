using NETCoreJsonMapper.Loggers.Utils;
using NETCoreJsonMapper.Properties;
using NETCoreJsonMapper.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace NETCoreJsonMapper.Builders
{
    internal class JsonClassResolver
    {
        private const string DLL_FILE_SEARCH_PATTERN = "*.dll";

        private static readonly AssemblyLoadContext assemblyLoadContext = AssemblyLoadContext.Default;

        private static readonly IList<Assembly> avaliableAssemblies = LoadSolutionAssemblies().ToList();

        internal static IEnumerable<Type> GetGenericIJsonDataSourceTypes(Type innerType)
        {
            DefaultLogger.Verbose(Resources.LOG_VERBOSE_RESOLVE_JSON_SOURCE_CLASS_FROM_ASSEMBLIES, innerType.FullName);
            foreach (Assembly assembly in avaliableAssemblies)
            {
                DefaultLogger.Verbose(Resources.LOG_VERBOSE_RESOLVE_JSON_SOURCE_CLASS_FROM_ASSEMBLY,
                    assembly.FullName, innerType.FullName);
                Type genericJsonDataSourceType = GetGenericIJsonDataSourceFromAssemblyOrDefault(
                    assembly: assembly, innerType: innerType);
                if (!TypeUtils.EqualsDefault(type: genericJsonDataSourceType))
                {
                    DefaultLogger.Verbose(Resources.LOG_VERBOSE_RESOLVE_JSON_SOURCE_CLASS_FROM_ASSEMBLY_VALID,
                        genericJsonDataSourceType.FullName, assembly.FullName, innerType.FullName);
                    yield return genericJsonDataSourceType;
                }
            }
        }

        internal static IEnumerable<Type> GetIJsonDataTargetTypes()
        {
            DefaultLogger.Verbose(Resources.LOG_VERBOSE_RESOLVE_JSON_TARGET_CLASS_FROM_ASSEMBLIES);
            foreach (Assembly assembly in avaliableAssemblies)
            {
                DefaultLogger.Verbose(Resources.LOG_VERBOSE_RESOLVE_JSON_TARGET_CLASS_FROM_ASSEMBLY, assembly.FullName);
                Type jsonDataTargetType = GetJsonDataTargetFromAssemblyOrDefault(assembly: assembly);
                if (!TypeUtils.EqualsDefault(type: jsonDataTargetType))
                {
                    DefaultLogger.Verbose(Resources.LOG_VERBOSE_RESOLVE_JSON_TARGET_CLASS_FROM_ASSEMBLY_VALID,
                        jsonDataTargetType.FullName, assembly.FullName);
                    yield return jsonDataTargetType;
                }
            }
        }

        private static Type GetGenericIJsonDataSourceFromAssemblyOrDefault(Assembly assembly, Type innerType)
        {
            return assembly.GetTypes()
                    .Where(t => IsTypeInheritsFromGenericJsonDataSourceInterface(type: t, innerType: innerType))
                    .FirstOrDefault();
        }

        private static Type GetJsonDataTargetFromAssemblyOrDefault(Assembly assembly)
        {
            return assembly.GetTypes()
                    .Where(IsTypeInheritsFromJsonDataTargetInterface)
                    .FirstOrDefault();
        }

        private static IEnumerable<string> GetSolutionAssemblyPaths() => Directory.EnumerateFiles(ReflectionUtils.AssemblyDirectory,
                                        DLL_FILE_SEARCH_PATTERN, SearchOption.TopDirectoryOnly);

        private static bool IsTypeInheritsFromGenericJsonDataSourceInterface(Type type, Type innerType)
        {
            bool result = true;
            result &= TypeUtils.IsAssignableFromGenericIJsonDataSource(
                type: type, innerType: innerType);
            result &= TypeUtils.IsInstanceable(type: type);
            return result;
        }

        private static bool IsTypeInheritsFromJsonDataTargetInterface(Type type)
        {
            bool result = true;
            result &= TypeUtils.IsAssignableFromIJsonDataTarget(type: type);
            result &= TypeUtils.IsInstanceable(type: type);
            return result;
        }

        private static IEnumerable<Assembly> LoadSolutionAssemblies()
        {
            DefaultLogger.Verbose(Resources.LOG_VERBOSE_RESOLVE_JSON_TARGET_CLASS_FROM_LOCAL_ASSEMBLIES);
            foreach (string assemblyFullPath in GetSolutionAssemblyPaths())
            {
                DefaultLogger.Verbose(Resources.LOG_VERBOSE_LOAD_LOCAL_ASSEMBLY_FROM_PATH, assemblyFullPath);
                yield return assemblyLoadContext.LoadFromAssemblyPath(assemblyPath: assemblyFullPath);
            }
        }
    }
}