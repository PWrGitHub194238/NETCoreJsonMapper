using NETCoreJsonMapper.Commons.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataSource : AJsonDataSource<JsonDataTarget>
    {
        #region Core properties

        [JsonProperty()]
        public List<ItemStructureSourceClass> ItemStructure { get; set; }

        #region ItemStructure

        [JsonObject()]
        public partial class ItemStructureSourceClass { }

        #region ItemStructure > PageInfo

        [JsonObject()]
        public partial class ItemSourceClass { }

        #region ItemStructure > PageInfo > Item > Template

        [JsonObject()]
        public partial class TemplateSourceClass { }

        #region ItemStructure > PageInfo > Item > Template > TemplateSection

        [JsonObject()]
        public partial class TemplateSectionSourceClass { }

        #region ItemStructure > PageInfo > Item > Template > TemplateSection > Field

        [JsonObject()]
        public partial class FieldSourceClass { }

        #region ItemStructure > PageInfo > Item > Template > TemplateSection > Field > FormatItem

        [JsonObject()]
        public partial class FormatItemSourceClass { }

        #endregion ItemStructure > PageInfo > Item > Template > TemplateSection > Field > FormatItem

        #endregion ItemStructure > PageInfo > Item > Template > TemplateSection > Field

        #endregion ItemStructure > PageInfo > Item > Template > TemplateSection

        #endregion ItemStructure > PageInfo > Item > Template

        #endregion ItemStructure > PageInfo

        #region ItemStructure > Renderings

        [JsonObject()]
        public partial class RenderingSourceClass { }

        #endregion ItemStructure > Renderings

        #region ItemStructure > Data

        #endregion ItemStructure > Data

        #endregion ItemStructure

        #endregion Core properties
    }
}
