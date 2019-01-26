using NETCoreJsonMapper.Common.Mappings;
using Newtonsoft.Json;

namespace Example2.Mappings
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonDataSource : AJsonDataSource<JsonDataTarget>
    {
        [JsonProperty()]
        public string ExampleProperty { get; set; }

        [JsonProperty()]
        public ExampleObjectClass ExampleObject { get; set; }

        [JsonObject()]
        public class ExampleObjectClass : AJsonDataSource<JsonDataTarget>
        {
            [JsonProperty()]
            public string ExampleObjectProperty { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void PostProcess()
        {
            base.PostProcess();
        }
    }
}
