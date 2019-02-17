using NETCoreJsonMapper.Commons.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;
using static TemplatesDemo.Mappings.JsonDataTarget;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataSource : AJsonDataSource<JsonDataTarget>
    {
        public partial class RenderingSourceClass
        {
            public RenderingSourceClass(RenderingTargetClass target)
            {

            }

            #region Properties

            [JsonProperty()]
            public string ID { get; set; }

            [JsonProperty()]
            public string Name { get; set; }

            [JsonProperty()]
            public string Placeholder { get; set; }

            [JsonProperty()]
            public string DataSource { get; set; }

            [JsonProperty()]
            public string Layout { get; set; }

            [JsonProperty()]
            public string Parameters { get; set; }

            #endregion Properties
        }
    }
}