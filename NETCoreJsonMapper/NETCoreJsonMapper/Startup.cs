using NETCoreJsonMapper.Builders;
using NETCoreJsonMapper.Loggers.Utils;
using NETCoreJsonMapper.Properties;
using NETCoreJsonMapper.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace NETCoreJsonMapper
{
    internal static class Startup
    {
        internal static void Execute(string outputDir, ISet<string> jsonFilePaths)
        {
            DefaultLogger.Verbose(Resources.LOG_VERBOSE_STARTUP_SCAN_FOR_TARGET_JSON_CLASS_TYPE);
            foreach (Type jsonDataTargetType in JsonClassResolver.GetIJsonDataTargetTypes())
            {
                DefaultLogger.Verbose(Resources.LOG_VERBOSE_STARTUP_SCAN_FOR_SOURCE_JSON_CLASS_TYPE, jsonDataTargetType);
                foreach (Type jsonDataSourceType in JsonClassResolver.GetGenericIJsonDataSourceTypes(innerType: jsonDataTargetType))
                {
                    DefaultLogger.Verbose(Resources.LOG_VERBOSE_STARTUP_PARSE_FOR_SOURCE_JSON_CLASS_TYPE,
                        jsonFilePaths, jsonDataSourceType, outputDir);
                    TryParseJson(jsonFilePaths: jsonFilePaths, sourceType: jsonDataSourceType, outputDir: outputDir);
                }
            }
        }

        private static void TryParseJson(ISet<string> jsonFilePaths, Type sourceType, string outputDir)
        {
            DefaultLogger.Verbose(Resources.LOG_VERBOSE_TRY_PARSE_JSON, jsonFilePaths, sourceType);
            ISet<string> mappedJsonFilePaths = new HashSet<string>();
            foreach (string jsonFilePath in jsonFilePaths)
            {
                JsonMapper jsonMapper = new JsonMapper(File.ReadAllText(jsonFilePath));
                DefaultLogger.Verbose(Resources.LOG_VERBOSE_TRY_PARSE_JSON_FROM_PATH, jsonFilePath, sourceType.FullName);
                if (jsonMapper.IsJsonMatchType(deserializationType: sourceType))
                {
                    DefaultLogger.Verbose(Resources.LOG_VERBOSE_TRY_PARSE_JSON_FROM_PATH_MATCH, jsonFilePath, sourceType.FullName);
                    string outputJsonString = jsonMapper.InvokeJsonMapping(deserializationType: sourceType);

                    mappedJsonFilePaths.Add(jsonFilePath);

                    File.WriteAllTextAsync(
                        Path.Combine(outputDir,
                        $"{sourceType.Assembly.GetName().Name}-{Path.GetFileNameWithoutExtension(jsonFilePath)}-result.json"),
                        outputJsonString);
                }
                else
                {
                    DefaultLogger.Verbose(Resources.LOG_VERBOSE_TRY_PARSE_JSON_FROM_PATH_NOT_MATCH, jsonFilePath, sourceType.FullName);
                }
            }
            jsonFilePaths.ExceptWith(mappedJsonFilePaths);
        }
    }
}