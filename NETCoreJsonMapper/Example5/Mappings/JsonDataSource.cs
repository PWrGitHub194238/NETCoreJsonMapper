using NETCoreJsonMapper.Common.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Example5.Mappings
{
    /// <summary>
    /// A sample class that represents a JSON formatted file ./JsonDataSource/Example.json.
    /// Along with this class, a JsonDataTarget was generated.
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
        public List<ItemClass> ExampleList1 { get; set; }

        [JsonObject()]
        public class ItemClass
        {
            [JsonProperty()]
            public int MyProperty { get; set; }
        }
    }
}