using NETCoreJsonMapper.Interfaces.Mappings;
using Newtonsoft.Json;
using static TemplatesDemo.Mappings.JsonDataSource;

namespace TemplatesDemo.Mappings
{
    public partial class JsonDataTarget : IJsonDataTarget
    {
        public partial class FormatItemTargetClass
        {
            public FormatItemTargetClass(FormatItemSourceClass source)
            {
                Index = source.Index;
                Value = source.Value;
            }

            #region Properties

            [JsonProperty()]
            public string Index { get; set; }

            [JsonProperty()]
            public string Value { get; set; }

            #endregion Properties

            #region Virtual

            #endregion Virtual
        }
    }
}