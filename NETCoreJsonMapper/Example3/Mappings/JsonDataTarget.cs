using NETCoreJsonMapper.Interface.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;
using static Example3.Mappings.JsonDataSource;

namespace Example3.Mappings
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
    /// 		"ExampleObjectProperty": "ExampleValue"
    /// 	},
    /// 	"ExampleArray": [
    /// 		"ExampleArrayValue1",
    /// 		"ExampleArrayValue2"
    /// 	],
    /// 	"ExampleObjectArray": [
    /// 		{
    /// 			"ExampleInnerProperty": "ExampleInnerProperty1Value0",
    /// 			"ExampleInnerObject": {
    /// 				"ExampleInnerProperty1": "ExampleInnerProperty1Value1",
    /// 				"ExampleInnerProperty2": null
    /// 			}
    /// 		},
    /// 		{
    /// 			"ExampleInnerProperty": "ExampleInnerProperty2Value0",
    /// 			"ExampleInnerObject": {
    /// 				"ExampleInnerProperty1": "ExampleInnerProperty2Value1",
    /// 				"ExampleInnerProperty2": "ExampleInnerProperty2Value2"
    /// 			}
    /// 		}
    /// 	]
    /// }
    /// 
    /// Each of the resulted JSON keys has a value 
    /// of the public property of this class enhanced by the proper JSON attribute.
    /// A data source for all the values generated is a JsonDataSource class.
    /// </summary>
    public class JsonDataTarget : IJsonDataTarget
    {
        [JsonProperty()]
        public string ExampleProperty { get; set; }

        [JsonProperty()]
        public ExampleObjectClass ExampleObject { get; set; }

        [JsonProperty()]
        public List<string> ExampleArray { get; set; }

        [JsonProperty()]
        public List<ExampleObjectArrayClass> ExampleObjectArray { get; set; }
    }
}
