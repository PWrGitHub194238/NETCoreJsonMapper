using TemplatesDemo.Attributes.SC9.ItemStructure.Page.PageInfo.Template;
using TemplatesDemo.Enums.SC9.ItemStructure.Page.PageInfo.Template;
using static TemplatesDemo.Mappings.JsonDataSource;
using static TemplatesDemo.Mappings.JsonDataTarget;

namespace TemplatesDemo.Conversions.SC9.ItemStructure.Page.PageInfo.Template
{
    [TemplateKeyValue(KeyValue = TemplateKeyValueEnum.ABSTRACT_COPY_COMPONENT)]
    internal class AbstractCopyComponentTemplate : TemplateTargetClass
    {
        public AbstractCopyComponentTemplate(TemplateSourceClass source) : base(source: source)
        {
            TemplatePath += " from AbstractCopyComponentTemplate!";
        }
    }
}
