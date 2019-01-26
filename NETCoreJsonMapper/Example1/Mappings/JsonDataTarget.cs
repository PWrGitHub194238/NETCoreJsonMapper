using NETCoreJsonMapper.Interface.Mappings;
using Newtonsoft.Json;

namespace Example1.Mappings
{
    /// <summary>
    /// A sample class that represents a JSON file to be generated out of this class' instance.
    /// Along with this class, a JsonDataSource was generated.
    /// 
    /// Example string that will be generated ased on this class:
    /// 
    /// {
    /// 	"FirstProperty": "FirstValue",
    /// 	"Second-Property": 3,
    /// 	"ThirdProperty": 40
    /// }
    /// 
    /// Each of the resulted JSON keys has a value 
    /// of the public property of this class enhanced by the proper JSON attribute.
    /// A data source for all the values generated is a JsonDataSource class.
    /// </summary>
    public class JsonDataTarget : IJsonDataTarget
    {
        /// <summary>
        /// The resulting JSON file will contain a key with the same name 
        /// as the class property name.
        /// </summary>
        [JsonProperty()]
        public string FirstProperty { get; set; }

        /// <summary>
        /// The resulting JSON file will contain a key with the same name 
        /// as the propertyName parameter of the JsonProperty attribute.
        /// </summary>
        [JsonProperty("Second-Property")]
        public int SecondProperty { get; set; }

        private int thirdProperty;

        /// <summary>
        /// The value of a key with the same name as the class property name
        /// can also be modified in a post process right before storing it to the resulting JSON file.
        /// </summary>
        [JsonProperty()]
        public int ThirdProperty {
            get => thirdProperty * 10;
            set => thirdProperty = value;
        }

    }
}
