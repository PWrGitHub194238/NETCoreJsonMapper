using NETCoreJsonMapper.Interfaces.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;
using static TemplatesDemo.Mappings.JsonDataSource;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataTarget : IJsonDataTarget
    {
        public partial class ItemStructureTargetClass
        {
            public ItemStructureTargetClass(ItemStructureSourceClass source)
            {
                PageInfo = new ItemTargetClass(source.PageInfo);

                Renderings = new List<RenderingTargetClass>();
                foreach (RenderingSourceClass rendering in source.Renderings)
                {
                    AddRendering(sourceRendering: rendering);
                }

                Data = new List<ItemTargetClass>();
                foreach (ItemSourceClass data in source.Data)
                {
                    AddData(sourceData: data);
                }
            }

            #region Properties

            [JsonProperty()]
            public ItemTargetClass PageInfo { get; set; }

            [JsonProperty()]
            public List<RenderingTargetClass> Renderings { get; set; }

            [JsonProperty()]
            public List<ItemTargetClass> Data { get; set; }

            #endregion Properties

            #region Virtual

            virtual protected void AddRendering(RenderingSourceClass sourceRendering)
                => Renderings.Add(new RenderingTargetClass(sourceRendering));

            virtual protected void AddData(ItemSourceClass sourceData)
                => Data.Add(new ItemTargetClass(sourceData));

            #endregion Virtual
        }
    }
}