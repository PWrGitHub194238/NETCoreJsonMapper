using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NETCoreJsonMapper.Utils
{
    internal static class FileUtils
    {
        public const string JSON_FILE_SEARCH_PATTERN = "*.json";

        internal static ISet<string> GetJsonDataSourceFileSet(List<string> inputDirList)
        {
            return GetJsonDataSourceFiles(inputDirList: inputDirList).Distinct().ToHashSet();
        }

        internal static IEnumerable<string> GetJsonDataSourceFiles(List<string> inputDirList)
        {
            foreach (string directoryPath in inputDirList)
            {
                foreach (string jsonFilePath in Directory.EnumerateFiles(directoryPath,
                    JSON_FILE_SEARCH_PATTERN, SearchOption.AllDirectories))
                {
                    yield return jsonFilePath;
                }
            }
        }
    }
}