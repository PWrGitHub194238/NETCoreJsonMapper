using NETCoreJsonMapper.Common.Mappings;

namespace Example2.Mappings
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
        public string ExampleProperty2 {
            set {
                jsonDataTarget.ExampleProperty2 = $"{value}-generated";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void PostProcess()
        {

        }
    }
}
