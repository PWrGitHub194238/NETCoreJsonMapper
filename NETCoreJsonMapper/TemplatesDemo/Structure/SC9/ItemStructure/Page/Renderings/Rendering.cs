using NETCoreJsonMapper.Interfaces.Mappings;
using Newtonsoft.Json;
using static TemplatesDemo.Mappings.JsonDataSource;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataTarget : IJsonDataTarget
    {
        public partial class RenderingTargetClass
        {
            public RenderingTargetClass(RenderingSourceClass source)
            {
                ID = source.ID;
                Name = source.Name;
                Placeholder = source.Placeholder;
                DataSource = source.DataSource;
                Layout = source.Layout;
                Parameters = source.Parameters;
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