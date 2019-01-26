using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NETCoreJsonMapper.Common.Utils
{
    internal class JsonUtils
    {
        private const string REGEX_JSON_KEY_MATCH_TOKEN = "JsonKey";
        private static readonly Regex JSON_KEY_MATCHER = new Regex($"^\\s*(?<{REGEX_JSON_KEY_MATCH_TOKEN}>[^\r\n:]+?)\\s*:",
            RegexOptions.Compiled
            | RegexOptions.Multiline);

        internal static ISet<string> GetJsonKeyCollection(string jsonString)
        {
            ISet<string> jsonKeySet = new HashSet<string>();
            MatchCollection matches = JSON_KEY_MATCHER.Matches(jsonString);
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                jsonKeySet.Add(GetJsonKeyFromGroupMatch(groupMatch: match.Groups));
            }
            return jsonKeySet;
        }

        internal static bool ValidateJsonWithClass(ISet<string> sourceJsonKeySet,
            ISet<string> targetClassKeySet)
        {
            bool result = true;
            result &= sourceJsonKeySet.Count > 0;
            result &= targetClassKeySet.Count > 0;
            result &= sourceJsonKeySet.Count == targetClassKeySet.Count;

            sourceJsonKeySet.ExceptWith(targetClassKeySet);
            result &= sourceJsonKeySet.Count == 0;

            return result;
        }

        private static string GetJsonKeyFromGroupMatch(GroupCollection groupMatch)
        {
            return groupMatch[REGEX_JSON_KEY_MATCH_TOKEN].Value
                .Replace("'", "")
                .Replace("\"", "");
        }
    }
}
