using SitecoreJsonMapper.Common.Utils;
using SitecoreJsonMapper.Interface.Mappings;
using System;

namespace SitecoreJsonMapper.Common.Mappings
{
    public abstract class AJsonDataSource<TJsonTarget> : IJsonDataSource<TJsonTarget>
        where TJsonTarget : new()
    {
        protected TJsonTarget jsonDataTarget;

        public AJsonDataSource()
        {
            jsonDataTarget = new TJsonTarget();
        }

        protected virtual void PostProcess()
        {

        }

        public bool IsValid(string jsonString)
        {
            return ReflectionUtils.ValidateJsonStringType(jsonString: jsonString, expectedType: GetType());
        }

        protected TJsonTarget GetResult()
        {
            PostProcess();
            return jsonDataTarget;
        }
    }
}
