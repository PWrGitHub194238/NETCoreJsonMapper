using System;
using TemplatesDemo.Enums.SC9.ItemStructure.Page.PageInfo.Template;

namespace TemplatesDemo.Attributes.SC9.ItemStructure.Page.PageInfo.Template
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class TemplateKeyValueAttribute : Attribute
    {
        public TemplateKeyValueEnum KeyValue { get; set; }
    }
}
