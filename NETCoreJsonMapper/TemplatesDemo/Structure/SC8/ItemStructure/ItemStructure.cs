using NETCoreJsonMapper.Commons.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataSource : AJsonDataSource<JsonDataTarget>
    {
        public partial class ItemStructureSourceClass
        {
            #region Properties

            [JsonProperty()]
            public ItemSourceClass PageInfo { get; set; }

            [JsonProperty()]
            public List<RenderingSourceClass> Renderings { get; set; }

            [JsonProperty()]
            public List<ItemSourceClass> Data { get; set; }

            #endregion Properties
        }
    }
}