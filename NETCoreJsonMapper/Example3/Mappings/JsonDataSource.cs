using NETCoreJsonMapper.Commons.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Example3.Mappings
{
    /// <summary>
    /// A sample class that represents a JSON formatted file ./JsonDataSource/Example.json.
    /// Along with this class, a JsonDataTarget was generated.
    ///
    /// All data loaded from the JSON file Example.json file:
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
    /// 	"ExampleObjectArray": [{
    /// 			"ExampleArrayObject1Key1": "ExampleArrayObject1Value1",
    /// 			"ExampleArrayObject1Key2": {
    /// 				"ExampleArrayProperty1": "ExampleArrayValue1"
    /// 			}
    /// 		},
    /// 		{
    /// 			"ExampleArrayObject2Key1": "ExampleArrayObject2Value1",
    /// 			"ExampleArrayObject2": {
    /// 				"ExampleArrayProperty1": "ExampleArrayValue1",
    /// 				"ExampleArrayProperty2": "ExampleArrayValue2"
    /// 			}
    /// 		}
    /// 	]
    /// }
    ///
    /// will be saved in the class properties whose name corresponds to the key names
    /// and object types from the specified file.
    ///
    /// To make the class visible for processing, it has to extend an AJsonDataSource<JsonDataTarget>
    /// class, where JsonDataTarget points the type of an object that represents
    /// the result JSON structure in a form of a class.
    /// </summary>
    public class JsonDataSource : AJsonDataSource<JsonDataTarget>
    {
        [JsonProperty()]
        public string ExampleProperty { get; set; }

        [JsonProperty()]
        public ExampleObjectClass ExampleObject { get; set; }

        [JsonProperty()]
        public List<string> ExampleArray { get; set; }

        [JsonProperty()]
        public List<ExampleObjectArrayClass> ExampleObjectArray { get; set; }

        [JsonObject()]
        public class ExampleObjectClass
        {
            [JsonProperty()]
            public string ExampleObjectProperty { get; set; }
        }

        [JsonObject()]
        public class ExampleObjectArrayClass
        {
            [JsonProperty()]
            public string ExampleInnerProperty { get; set; }

            [JsonProperty()]
            public ExampleInnerObjectClass ExampleInnerObject { get; set; }

            [JsonObject()]
            public class ExampleInnerObjectClass
            {
                [JsonProperty()]
                public string ExampleInnerProperty1 { get; set; }

                [JsonProperty()]
                public string ExampleInnerProperty2 { get; set; }
            }
        }
    }
}