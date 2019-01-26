using NETCoreJsonMapper.Common.Mappings;
using Newtonsoft.Json;

namespace Example1.Mappings
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonDataSource : AJsonDataSource<JsonDataTarget>
    {
        /// <summary>
        /// Put all properties of a generated RootObject class here.
        /// You can set JsonDataTarget properties by the set accessor.
        /// </summary>
        [JsonProperty()]
        public string ExampleProperty {
            set {
                jsonDataTarget.ExampleProperty = $"{value}-generated";
            }
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
