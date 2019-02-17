using NETCoreJsonMapper.Commons.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataSource : AJsonDataSource<JsonDataTarget>
    {
        public partial class TemplateSectionSourceClass
        {
            #region Properties

            [JsonProperty()]
            public string Name { get; set; }

            [JsonProperty()]
            public List<FieldSourceClass> Fields { get; set; }

            #endregion Properties
        }
    }
}