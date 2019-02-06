using NETCoreJsonMapper.Properties;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NETCoreJsonMapper.Utils
{
    internal static class FileUtils
    {
        public const string JSON_FILE_SEARCH_PATTERN = "*.json";

        internal static IEnumerable<string> GetJsonDataSourceFiles(List<string> inputDirList)
        {
            foreach (string directoryPath in inputDirList)
            {
                foreach (string jsonFilePath in Directory.EnumerateFiles(directoryPath,
                    JSON_FILE_SEARCH_PATTERN, SearchOption.AllDirectories))
                {
                    LogUtils.Logger.Verbose(Resources.LOG_VERBOSE_CMD_LINE_YIELD_JSON_SOURCE_FILE_PATH,
                        jsonFilePath, directoryPath);
                    yield return jsonFilePath;
                }
            }
        }

        internal static ISet<string> GetJsonDataSourceFileSet(List<string> inputDirList) => GetJsonDataSourceFiles(
                    inputDirList: inputDirList).Distinct().ToHashSet();
    }
}