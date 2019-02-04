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
            foreach (Type jsonDataTargetType in ReflectionUtils.GetIJsonDataTargetTypes())
            {
                foreach (Type jsonDataSourceType in ReflectionUtils.GetGenericIJsonDataSourceTypes(innerType: jsonDataTargetType))
                {
                    TryParseJson(jsonFilePaths: jsonFilePaths, sourceType: jsonDataSourceType, outputDir: outputDir);
                }
            }
        }

        private static void TryParseJson(ISet<string> jsonFilePaths, Type sourceType, string outputDir)
        {
            ISet<string> mappedJsonFilePaths = new HashSet<string>();
            foreach (string jsonFilePath in jsonFilePaths)
            {
                string jsonString = File.ReadAllText(jsonFilePath);
                if (ReflectionUtils.IsJsonMatchType(jsonString: jsonString, deserializationType: sourceType))
                {
                    string outputJsonString = ReflectionUtils.InvokeJsonMapping(jsonString: jsonString,
                        deserializationType: sourceType);

                    mappedJsonFilePaths.Add(jsonFilePath);

                    File.WriteAllTextAsync(
                        Path.Combine(outputDir,
                        $"{sourceType.Assembly.GetName().Name}-{Path.GetFileNameWithoutExtension(jsonFilePath)}-result.json"),
                        outputJsonString);
                }
            }
            jsonFilePaths.ExceptWith(mappedJsonFilePaths);
        }
    }
}