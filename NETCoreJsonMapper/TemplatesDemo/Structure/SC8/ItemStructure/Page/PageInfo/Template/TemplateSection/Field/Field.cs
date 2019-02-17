using NETCoreJsonMapper.Commons.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataSource : AJsonDataSource<JsonDataTarget>
    {
        public partial class FieldSourceClass
        {
            #region Properties

            [JsonProperty()]
            public string Name { get; set; }

            [JsonProperty()]
            public string Type { get; set; }

            [JsonProperty()]
            public string Value { get; set; }

            [JsonProperty()]
            public List<FormatItemSourceClass> FormatItem { get; set; }

            #endregion Properties
        }
    }
}