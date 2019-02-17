using NETCoreJsonMapper.Commons.Mappings;
using Newtonsoft.Json;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataSource : AJsonDataSource<JsonDataTarget>
    {
        public partial class ItemSourceClass
        {
            #region Properties

            [JsonProperty()]
            public string ID { get; set; }

            [JsonProperty()]
            public string DisplayName { get; set; }

            [JsonProperty()]
            public string ParentPath { get; set; }

            [JsonProperty()]
            public string Language { get; set; }

            [JsonProperty()]
            public TemplateSourceClass Template { get; set; }

            #endregion Properties
        }
    }
}