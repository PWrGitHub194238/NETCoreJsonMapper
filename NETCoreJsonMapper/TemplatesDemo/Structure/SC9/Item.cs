using NETCoreJsonMapper.Interfaces.Mappings;
using Newtonsoft.Json;
using TemplatesDemo.Conversions.SC9.ItemStructure.Page.PageInfo.Template;
using TemplatesDemo.Utils;
using static TemplatesDemo.Mappings.JsonDataSource;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataTarget : IJsonDataTarget
    {
        public partial class ItemTargetClass
        {
            public ItemTargetClass(ItemSourceClass source)
            {
                ID = source.ID;
                DisplayName = source.DisplayName;
                ParentPath = source.ParentPath;
                Language = source.Language;
                //Template = new TemplateTargetClass(source: source.Template);

                ReflectionUtils.GetAttrBasedConstructor(this, typeof(TemplateTargetClass), source.Template);
                //Template = new AbstractCopyComponentTemplate(source: source.Template);
            }

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
            public TemplateTargetClass Template { get; set; }

            #endregion Properties
        }
    }
}