using NETCoreJsonMapper.Common.Mappings;
using Newtonsoft.Json;

namespace Example2.Mappings
{
    /// <summary>
    /// A sample class that represents a JSON formatted file ./JsonDataSource/Example.json.
    /// Along with this class, a JsonDataTarget was generated.
    /// 
    /// All data loaded from the JSON file Example.json file:
    /// 
    /// {
    ///		"ExampleProperty": "ExampleValue",
    ///		"ExampleObject": {
    ///			"ExampleObjectProperty": "ExampleInnerValue"
    ///		}
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
        /// <summary>
        /// For each key in a JSON file, generate a property with a JsonProperty attribute.
        /// Name of each property will be matched against JsonDataTarget class and its value 
        /// will be assigned to the property of that class with the same name by default.
        /// </summary>
        [JsonProperty()]
        public string OuterProperty { get; set; }

        /// <summary>
        /// Each nested JSON object has to be represented as a class property of a valid type.
        /// </summary>
        [JsonProperty()]
        public OuterObjectClass OuterObject { get; set; }

        [JsonProperty()]
        public SecondOuterObjectClass SecondOuterObject { get; set; }

        /// <summary>
        /// Each of inner types has to be enhanced  by a JsonObject attribute. 
        /// </summary>
        [JsonObject()]
        public class OuterObjectClass
        {
            [JsonProperty()]
            public string InnerProperty { get; set; }
        }

        /// <summary>
        /// Nested types can be defined recursively as shown below.
        /// </summary>
        [JsonObject()]
        public class SecondOuterObjectClass
        {
            [JsonProperty()]
            public string SecondInnerProperty { get; set; }

            [JsonProperty()]
            public OuterObjectClass OuterObject { get; set; }

            [JsonProperty()]
            public SecondOuterObjectClass ChildObject { get; set; }
        }
    }
}
