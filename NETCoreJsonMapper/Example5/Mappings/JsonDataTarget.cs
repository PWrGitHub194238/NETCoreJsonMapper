using NETCoreJsonMapper.Interface.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Example5.Mappings
{
    /// <summary>
    /// A sample class that represents a JSON file to be generated out of this class' instance.
    /// Along with this class, a JsonDataSource was generated.
    ///
    /// Each of the resulted JSON keys has a value
    /// of the public property of this class enhanced by the proper JSON attribute.
    /// A data source for all the values generated is a JsonDataSource class.
    /// </summary>
    public class JsonDataTarget : IJsonDataTarget
    {
        [JsonProperty()]
        public List<TargetItemClass> ExampleList1 { get; set; }

        [JsonObject()]
        public class TargetItemClass
        {
            public TargetItemClass(JsonDataSource.ItemClass item)
            {
                MyProperty = item.MyProperty * 2;
            }

            [JsonProperty()]
            public int MyProperty { get; set; }
        }
    }
}