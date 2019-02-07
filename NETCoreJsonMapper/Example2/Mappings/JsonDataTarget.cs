using NETCoreJsonMapper.Interfaces.Mappings;
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
    /// 	"OuterProperty": "OuterValue",
    /// 	"OuterObject-1": {
    /// 		"InnerProperty": "FirstInnerValue-default"
    /// 	},
    /// 	"OuterObject-3": {
    /// 		"InnerProperty": "ThirdInnerValue-default-generated-default"
    /// 	},
    /// 	"OuterObject-1-1": {
    /// 		"SecondInnerProperty": 2,
    /// 		"OuterObject": {
    /// 			"InnerProperty": "InnerValue1-default"
    /// 		},
    /// 		"ChildObject": {
    /// 			"SecondInnerProperty": 4,
    /// 			"OuterObject": {
    /// 				"InnerProperty": "-empty"
    /// 			},
    /// 			"ChildObject": null
    /// 		}
    /// 	},
    /// 	"OuterObject-2": null
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
        public OuterObjectClass FirstOuterObject { get; set; }

        [JsonProperty("OuterObject-2")]
        public OuterObjectClass SecondOuterObject { get; set; }

        [JsonProperty("OuterObject-3")]
        public OuterObjectClass ThirdOuterObject { get; set; }

        [JsonProperty("OuterObject-1-1")]
        public SecondOuterObjectClass FourthOuterObject { get; set; }
    }
}