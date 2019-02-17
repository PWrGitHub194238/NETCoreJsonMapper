using TemplatesDemo.Attributes.SC9.ItemStructure.Page.PageInfo.Template;
using TemplatesDemo.Enums.SC9.ItemStructure.Page.PageInfo.Template;
using static TemplatesDemo.Mappings.JsonDataSource;
using static TemplatesDemo.Mappings.JsonDataTarget;

namespace TemplatesDemo.Conversions.SC9.ItemStructure.Page.PageInfo.Template
{
    [TemplateKeyValue(KeyValue = TemplateKeyValueEnum.QUOTATION_COPY_COMPONENT)]
    internal class QuotationCopyComponentTemplate : TemplateTargetClass
    {
        public QuotationCopyComponentTemplate(TemplateSourceClass source) : base(source: source)
        {
            TemplatePath += " from QuotationCopyComponentTemplate!";
        }

        protected override void AddTemplateSection(TemplateSectionSourceClass sourceTemplateSection)
            => TemplateSections.Add(new TemplateSectionTargetClass(sourceTemplateSection));
    }
}
