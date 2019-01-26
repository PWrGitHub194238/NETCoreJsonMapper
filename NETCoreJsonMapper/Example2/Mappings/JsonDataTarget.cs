using NETCoreJsonMapper.Interface.Mappings;
using Newtonsoft.Json;

namespace Example2.Mappings
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonDataTarget : IJsonDataTarget
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty()]
        public string ExampleProperty2 { get; set; }
    }
}
