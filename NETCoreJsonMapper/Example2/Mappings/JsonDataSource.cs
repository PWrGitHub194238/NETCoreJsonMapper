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
    ///   "OuterProperty": "OuterValue",
    ///   "FirstOuterObject": {
    ///     "InnerProperty": "FirstInnerValue"
    ///   },
    ///   "SecondOuterObject": {
    ///     "InnerProperty": "SecondInnerValue"
    ///   },
    ///   "ThirdOuterObject": {
    ///     "InnerProperty": "ThirdInnerValue"
    ///   },
    ///   "FourthOuterObject": {
    ///     "SecondInnerProperty": 1,
    ///     "OuterObject": {
    ///       "InnerProperty": "InnerValue1"
    ///     },
    ///     "ChildObject": {
    ///       "SecondInnerProperty": 2,
    ///       "OuterObject": {
    ///         "InnerProperty": "InnerValue2"
    ///       },
    ///       "ChildObject": null
    ///     }
    ///   }
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
        /// Name of each property will be matched against JsonDataTarget class and its value 
        /// will be assigned to the property of that class with the same name by default.
        /// </summary>
        [JsonProperty()]
        public OuterObjectClass FirstOuterObject { get; set; }

        /// <summary>
        /// Each nested JSON object has to be represented as a class property of a valid type.
        /// A custom constructor can be implemented and use instead of a default one.
        /// To specify a default getter accessor, full property have to be used.
        /// If not specified, it is entirely ignored while generating the result JSON.
        /// </summary>
        [JsonProperty()]
        public OuterObjectClass SecondOuterObject {
            set => new OuterObjectClass("generated");
        }

        private OuterObjectClass thirdOuterObject;

        /// <summary>
        /// Each nested JSON object has to be represented as a class property of a valid type.
        /// In a setter acessor the inner object can be further modified after assignin its default value, 
        /// generated from the data source JSON file.
        /// </summary>
        [JsonProperty()]
        public OuterObjectClass ThirdOuterObject {
            get => thirdOuterObject;
            set {
                thirdOuterObject = value;
                thirdOuterObject.InnerProperty += "-generated";
            }
        }

        [JsonProperty()]
        public SecondOuterObjectClass FourthOuterObject { get; set; }

        /// <summary>
        /// Each of inner types has to be enhanced by a JsonObject attribute. 
        /// Name of each nested class will be matched against JsonDataTarget class and its value 
        /// will be assigned to the property of that class with the same name by default recursively.
        /// </summary>
        [JsonObject()]
        public class OuterObjectClass
        {
            private readonly string exampleValue;

            public OuterObjectClass() : this("default")
            {

            }

            public OuterObjectClass(string exampleValue) => this.exampleValue = exampleValue;

            /// <summary>
            /// 
            /// </summary>
            private string innerProperty;

            /// <summary>
            /// For each key in a JSON file, generate a property with a JsonProperty attribute.
            /// Name of each property will be matched against JsonDataTarget class and its value 
            /// will be assigned to the property of that class with the same name by default.
            /// If a value of the property is set manualy from outside, the getter accessor 
            /// can be used to further modified a value on top of already set value.
            /// </summary>
            [JsonProperty()]
            public string InnerProperty {
                get => $"{innerProperty}-{exampleValue}";
                set => innerProperty = value;
            }
        }

        /// <summary>
        /// Each of inner types has to be enhanced by a JsonObject attribute. 
        /// Name of each nested class will be matched against JsonDataTarget class and its value 
        /// will be assigned to the property of that class with the same name by default recursively.
        /// Nested types can be defined recursively as shown below.
        /// </summary>
        [JsonObject()]
        public class SecondOuterObjectClass
        {
            public static int counter = 0;
            private int secondInnerProperty;

            /// <summary>
            /// For each key in a JSON file, generate a property with a JsonProperty attribute.
            /// Name of each property will be matched against JsonDataTarget class and its value 
            /// will be assigned to the property of that class with the same name by default.
            /// To change the resulted JSON value of any key to a relative value,
            /// a setter accessor has to be defined. The getter accessor will benefit from 
            /// the value stored in a private field that will not be used to generate resulted JSON.
            /// </summary>
            [JsonProperty()]
            public int SecondInnerProperty {
                get => secondInnerProperty * 2;
                set => secondInnerProperty = value;
            }

            private OuterObjectClass outerObject;

            /// <summary>
            /// Each nested JSON object has to be represented as a class property of a valid type.
            /// Name of each property will be matched against JsonDataTarget class and its value 
            /// will be assigned to the property of that class with the same name by default.
            /// Each reference type property can be validated agains a null value.
            /// In this example, in case of the null value, a new object will be returned.
            /// Be aware that by modifying getter accessor and not allow to return null, 
            /// nested values of a given type will NOT be mapped.
            /// </summary>
            [JsonProperty()]
            public OuterObjectClass OuterObject {
                get => new OuterObjectClass($"{outerObject?.InnerProperty}-empty{counter++}");
                set => outerObject = value;
            }

            /// <summary>
            /// Each nested JSON object has to be represented as a class property of a valid type.
            /// Name of each property will be matched against JsonDataTarget class and its value 
            /// will be assigned to the property of that class with the same name by default.
            /// </summary>
            [JsonProperty()]
            public SecondOuterObjectClass ChildObject { get; set; }
        }
    }
}