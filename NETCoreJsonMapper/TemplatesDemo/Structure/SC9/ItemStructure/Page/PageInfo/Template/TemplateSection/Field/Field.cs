using NETCoreJsonMapper.Interfaces.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;
using static TemplatesDemo.Mappings.JsonDataSource;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataTarget : IJsonDataTarget
    {
        public partial class FieldTargetClass
        {
            public FieldTargetClass(FieldSourceClass source)
            {
                Name = source.Name;
                Type = source.Type;
                Value = source.Value;
                FormatItem = new List<FormatItemTargetClass>();

                foreach (FormatItemSourceClass formatItem in source.FormatItem)
                {
                    AddFormatItem(sourceFormatItem: formatItem);
                }
            }

            #region Properties

            [JsonProperty()]
            public string Name { get; set; }

            [JsonProperty()]
            public string Type { get; set; }

            [JsonProperty()]
            public string Value { get; set; }

            [JsonProperty()]
            public List<FormatItemTargetClass> FormatItem { get; set; }

            #endregion Properties

            #region Virtual

            virtual protected void AddFormatItem(FormatItemSourceClass sourceFormatItem)
                => FormatItem.Add(new FormatItemTargetClass(sourceFormatItem));

            #endregion Virtual
        }
    }
}