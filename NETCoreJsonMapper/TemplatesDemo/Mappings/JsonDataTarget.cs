using NETCoreJsonMapper.Interfaces.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataTarget : IJsonDataTarget
    {
        #region Core properties

        [JsonProperty()]
        public List<ItemStructureTargetClass> ItemStructure { get; set; }

        #region ItemStructure

        [JsonObject()]
        public partial class ItemStructureTargetClass { }

        #region ItemStructure > PageInfo

        [JsonObject()]
        public partial class ItemTargetClass { }

        #region ItemStructure > PageInfo > Item > Template

        [JsonObject()]
        public partial class TemplateTargetClass { }

        #region ItemStructure > PageInfo > Item > Template > TemplateSection

        [JsonObject()]
        public partial class TemplateSectionTargetClass { }

        #region ItemStructure > PageInfo > Item > Template > TemplateSection > Field

        [JsonObject()]
        public partial class FieldTargetClass { }

        #region ItemStructure > PageInfo > Item > Template > TemplateSection > Field > FormatItem

        [JsonObject()]
        public partial class FormatItemTargetClass { }

        #endregion ItemStructure > PageInfo > Item > Template > TemplateSection > Field > FormatItem

        #endregion ItemStructure > PageInfo > Item > Template > TemplateSection > Field

        #endregion ItemStructure > PageInfo > Item > Template > TemplateSection

        #endregion ItemStructure > PageInfo > Item > Template

        #endregion ItemStructure > PageInfo

        #region ItemStructure > Renderings

        [JsonObject()]
        public partial class RenderingTargetClass { }

        #endregion ItemStructure > Renderings

        #region ItemStructure > Data

        #endregion ItemStructure > Data

        #endregion ItemStructure

        #endregion Core properties
    }
}
