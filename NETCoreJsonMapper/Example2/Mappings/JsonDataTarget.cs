using NETCoreJsonMapper.Interface.Mappings;
using Newtonsoft.Json;
using static Example2.Mappings.JsonDataSource;

namespace Example2.Mappings
{
    /// <summary>
    /// A sample class that represents a JSON file to be generated out of this class' instance.
    /// Along with this class, a JsonDataSource was generated.
    /// 
    /// Example string that will be generated ased on this class:
    /// 
    /// {
    /// 	"ExampleProperty": "ExampleValue",
    /// 	"ExampleObject": {
    /// 		"ExampleObjectProperty": "ExampleInnerValue"
    /// 	}
    /// }
    /// 
    /// Each of the resulted JSON keys has a value 
    /// of the public property of this class enhanced by the proper JSON attribute.
    /// A data source for all the values generated is a JsonDataSource class.
    /// </summary>
    public class JsonDataTarget : IJsonDataTarget
    {
        [JsonProperty("OuterProperty")]
        public string OuterProperty { get; set; }

        [JsonProperty("OuterObject-1")]
        public OuterObjectClass OuterObject { get; set; }

        [JsonProperty("OuterObject-1-1")]
        public SecondOuterObjectClass SecondOuterObject { get; set; }
    }
}
