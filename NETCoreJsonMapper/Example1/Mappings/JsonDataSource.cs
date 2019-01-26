using NETCoreJsonMapper.Common.Mappings;
using Newtonsoft.Json;

namespace Example1.Mappings
{
    /// <summary>
    /// A sample class that represents a JSON formatted file ./JsonDataSource/Example.json.
    /// Along with this class, a JsonDataTarget was generated.
    /// 
    /// All data loaded from the JSON file Example.json file:
    /// 
    /// {
    /// 	"FirstProperty": "FirstValue",
    /// 	"SecondProperty": 2,
    /// 	"ThirdProperty": 3
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
        public string FirstProperty { get; set; }

        /// <summary>
        /// To change the resulted JSON value of any key to a constant value,
        /// at least the get accessor has to be modified.
        /// Name of each property will be matched against JsonDataTarget class and its value 
        /// will be assigned to the property of that class with the same name by default.
        /// </summary>
        [JsonProperty()]
        public int SecondProperty {
            get => 3;
        }

        private int thirdProperty;

        /// <summary>
        /// To change the resulted JSON value of any key to a relative value,
        /// a setter accessor has to be defined. The getter accessor will benefit from 
        /// the value stored in a private field that will not be used to generate resulted JSON.
        /// </summary>
        [JsonProperty()]
        public int ThirdProperty {
            get => thirdProperty + 1;
            set => thirdProperty = value;
        }

    }
}
