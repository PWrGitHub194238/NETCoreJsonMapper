using NETCoreJsonMapper.Commons.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataSource : AJsonDataSource<JsonDataTarget>
    {
        public partial class TemplateSourceClass
        {
            #region Properties

            [JsonProperty()]
            public string TemplatePath { get; set; }

            [JsonProperty()]
            public List<TemplateSectionSourceClass> TemplateSections { get; set; }

            #endregion Properties
        }
    }
}