using NETCoreJsonMapper.Interfaces.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;
using static TemplatesDemo.Mappings.JsonDataSource;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataTarget : IJsonDataTarget
    {
        public partial class TemplateSectionTargetClass
        {
            public TemplateSectionTargetClass(TemplateSectionSourceClass source)
            {
                Name = source.Name;
                Fields = new List<FieldTargetClass>();

                foreach (FieldSourceClass field in source.Fields)
                {
                    AddField(sourceField: field);
                }
            }

            #region Properties

            [JsonProperty()]
            public string Name { get; set; }

            [JsonProperty()]
            public List<FieldTargetClass> Fields { get; set; }

            #endregion Properties

            #region Virtual

            virtual protected void AddField(FieldSourceClass sourceField)
                => Fields.Add(new FieldTargetClass(sourceField));

            #endregion Virtual
        }
    }
}