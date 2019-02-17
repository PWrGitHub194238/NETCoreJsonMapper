using NETCoreJsonMapper.Commons.Mappings;
using Newtonsoft.Json;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataSource : AJsonDataSource<JsonDataTarget>
    {
        public partial class FormatItemSourceClass
        {
            #region Properties

            [JsonProperty()]
            public string Index { get; set; }

            [JsonProperty()]
            public string Value { get; set; }

            #endregion Properties
        }
    }
}