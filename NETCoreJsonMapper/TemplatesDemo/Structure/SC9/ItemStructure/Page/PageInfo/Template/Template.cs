using NETCoreJsonMapper.Interfaces.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;
using TemplatesDemo.Attributes.SC9.ItemStructure.Page.PageInfo.Template;
using static TemplatesDemo.Mappings.JsonDataSource;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataTarget : IJsonDataTarget
    {
        [TemplateKeyField(KeyField = "TemplatePath")]
        public partial class TemplateTargetClass
        {
            public TemplateTargetClass(TemplateSourceClass source)
            {
                TemplatePath = source.TemplatePath;
                TemplateSections = new List<TemplateSectionTargetClass>();

                foreach (TemplateSectionSourceClass template in source.TemplateSections)
                {
                    AddTemplateSection(sourceTemplateSection: template);
                }
            }

            #region Properties

            [JsonProperty()]
            public string TemplatePath { get; set; }

            [JsonProperty()]
            public List<TemplateSectionTargetClass> TemplateSections { get; set; }

            #endregion Properties

            #region Virtual

            virtual protected void AddTemplateSection(TemplateSectionSourceClass sourceTemplateSection)
                => TemplateSections.Add(new TemplateSectionTargetClass(sourceTemplateSection));

            #endregion Virtual
        }
    }
}