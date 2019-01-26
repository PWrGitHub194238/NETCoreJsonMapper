using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace SitecoreJsonMapper.Utils
{
    internal static class ReflectionUtils
    {
        private const string DLL_FILE_SEARCH_PATTERN = "*.dll";

        private const string DESERIALIZE_OBJECT_METHOD_NAME = "DeserializeObject";
        private const string SERIALIZE_OBJECT_METHOD_NAME = "SerializeObject";
        private const string IS_VALID_METHOD_NAME = "IsValid";
        private const string GET_RESULT_METHOD_NAME = "GetResult";

        private static readonly AssemblyLoadContext assemblyLoadContext = AssemblyLoadContext.Default;

        private static readonly MethodInfo JsonConvertDeserializeMethodInfo = typeof(JsonConvert)
            .GetMethods(
                BindingFlags.Public
                    | BindingFlags.Static
                    | BindingFlags.OptionalParamBinding
                    | BindingFlags.InvokeMethod)
            .Where(
                m => m.IsGenericMethod
                && m.ContainsGenericParameters
                && m.Name == DESERIALIZE_OBJECT_METHOD_NAME)
            .FirstOrDefault();

        private static readonly MethodInfo JsonConvertSerializeMethodInfo = typeof(JsonConvert)
            .GetMethod(SERIALIZE_OBJECT_METHOD_NAME,
                BindingFlags.Public
                    | BindingFlags.Static
                    | BindingFlags.OptionalParamBinding
                    | BindingFlags.InvokeMethod,
                Type.DefaultBinder,
                new[] {
                    typeof(string)
                },
                null);

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

        public static string InvokeJsonMapping(string jsonString, Type deserializationType)
        {
            object deserializedJsonObject = GetDeserializationResult(jsonString: jsonString,
                deserializationType: deserializationType);
            return JsonConvertSerializeMethodInfo
                .Invoke(null, new object[] {
                    deserializedJsonObject
                }).ToString();
        }

        public static bool IsJsonMatchType(string jsonString, Type deserializationType)
        {
            MethodInfo isValidMethod = deserializationType.GetMethod(IS_VALID_METHOD_NAME,
               BindingFlags.Instance | BindingFlags.Public);
            return (bool)isValidMethod.Invoke(Activator.CreateInstance(deserializationType),
                new object[] { jsonString });
        }

        private static object GetDeserializationResult(string jsonString, Type deserializationType)
        {
            object deserializedJsonObjectWrapper = InvokeDeserialize(jsonString: jsonString,
                deserializationType: deserializationType);
            return deserializedJsonObjectWrapper.GetType().GetMethod(GET_RESULT_METHOD_NAME,
                BindingFlags.Instance | BindingFlags.NonPublic)
                .Invoke(deserializedJsonObjectWrapper, null);
        }

        private static object InvokeDeserialize(string jsonString, Type deserializationType)
        {
            return JsonConvertDeserializeMethodInfo.MakeGenericMethod(deserializationType)
                .Invoke(null, new object[] { jsonString });
        }
    }
}
