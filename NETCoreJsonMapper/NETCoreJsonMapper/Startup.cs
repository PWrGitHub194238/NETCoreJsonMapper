using NETCoreJsonMapper.Builders;
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
            LogUtils.Logger.Verbose(Resources.LOG_VERBOSE_STARTUP_SCAN_FOR_TARGET_JSON_CLASS_TYPE);
            foreach (Type jsonDataTargetType in ReflectionUtils.GetIJsonDataTargetTypes())
            {
                LogUtils.Logger.Verbose(Resources.LOG_VERBOSE_STARTUP_SCAN_FOR_SOURCE_JSON_CLASS_TYPE, jsonDataTargetType);
                foreach (Type jsonDataSourceType in ReflectionUtils.GetGenericIJsonDataSourceTypes(innerType: jsonDataTargetType))
                {
                    LogUtils.Logger.Verbose(Resources.LOG_VERBOSE_STARTUP_PARSE_FOR_SOURCE_JSON_CLASS_TYPE,
                        jsonFilePaths, jsonDataSourceType, outputDir);
                    TryParseJson(jsonFilePaths: jsonFilePaths, sourceType: jsonDataSourceType, outputDir: outputDir);
                }
            }
        }

        private static void TryParseJson(ISet<string> jsonFilePaths, Type sourceType, string outputDir)
        {
            ISet<string> mappedJsonFilePaths = new HashSet<string>();
            foreach (string jsonFilePath in jsonFilePaths)
            {
                JsonMapper jsonMapper = new JsonMapper(File.ReadAllText(jsonFilePath));
                LogUtils.Logger.Verbose(Resources.LOG_VERBOSE_TRY_PARSE_JSON_FROM_PATH, jsonFilePath);
                if (jsonMapper.IsJsonMatchType(deserializationType: sourceType))
                {
                    LogUtils.Logger.Verbose(Resources.LOG_VERBOSE_TRY_PARSE_JSON_FROM_PATH_MATCH, jsonFilePath, sourceType);
                    string outputJsonString = jsonMapper.InvokeJsonMapping(deserializationType: sourceType);

                    mappedJsonFilePaths.Add(jsonFilePath);

                    File.WriteAllTextAsync(
                        Path.Combine(outputDir,
                        $"{sourceType.Assembly.GetName().Name}-{Path.GetFileNameWithoutExtension(jsonFilePath)}-result.json"),
                        outputJsonString);
                }
                else
                {
                    LogUtils.Logger.Verbose(Resources.LOG_VERBOSE_TRY_PARSE_JSON_FROM_PATH_NOT_MATCH, jsonFilePath, sourceType);
                }
            }
            jsonFilePaths.ExceptWith(mappedJsonFilePaths);
        }
    }
}