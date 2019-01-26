using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NETCoreJsonMapper.Common.Utils
{
    internal class ReflectionUtils
    {
        /// <summary>
        /// Checks whether a given jsonString can be deserialized onto a given expectedType.
        /// </summary>
        /// <param name="jsonString">Well formatted JSON string.</param>
        /// <param name="expectedType">The expected type of the giver JSON string deserialization's result.
        /// </param>
        /// <returns></returns>
        internal static bool ValidateJsonStringType(string jsonString, Type expectedType)
        {
            ISet<string> jsonKeySet = JsonUtils.GetJsonKeyCollection(jsonString: jsonString);
            ISet<string> classKeySet = GetClassPropertyCollection(jsonType: expectedType);
            return JsonUtils.ValidateJsonWithClass(sourceJsonKeySet: jsonKeySet,
                targetClassKeySet: classKeySet);
        }

        private static ISet<string> GetClassPropertyCollection(Type jsonType)
        {
            return jsonType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => Attribute.IsDefined(p, typeof(JsonPropertyAttribute)))
                .Select(p => p.Name).ToHashSet();
        }
    }
}
