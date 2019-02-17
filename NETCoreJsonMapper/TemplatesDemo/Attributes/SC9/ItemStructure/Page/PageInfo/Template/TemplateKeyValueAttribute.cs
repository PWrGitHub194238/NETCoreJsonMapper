using System;

namespace TemplatesDemo.Attributes.SC9.ItemStructure.Page.PageInfo.Template
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class TemplateKeyFieldAttribute : Attribute
    {
        public string KeyField { get; set; }
    }
}
