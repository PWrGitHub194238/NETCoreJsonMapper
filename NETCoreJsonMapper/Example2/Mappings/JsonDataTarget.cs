using NETCoreJsonMapper.Interface.Mappings;
using Newtonsoft.Json;
using static Example2.Mappings.JsonDataSource;

namespace Example2.Mappings
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonDataTarget : IJsonDataTarget
    {
        [JsonProperty()]
        public string ExampleProperty { get; set; }

        [JsonProperty()]
        public ExampleObjectClass ExampleObject { get; set; }
    }
}
