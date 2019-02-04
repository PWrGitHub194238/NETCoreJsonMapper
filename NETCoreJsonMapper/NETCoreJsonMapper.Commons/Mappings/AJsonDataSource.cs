using NETCoreJsonMapper.Common.Utils;
using NETCoreJsonMapper.Interface.Mappings;
using System;

namespace NETCoreJsonMapper.Common.Mappings
{
    public abstract class AJsonDataSource<TJsonTarget> : IJsonDataSource<TJsonTarget>
        where TJsonTarget : IJsonDataTarget, new()
    {
        protected TJsonTarget jsonDataTarget;
        private readonly Type jsonDataSourceType;
        private readonly Type jsonDataTargetType;

        public AJsonDataSource()
        {
            jsonDataSourceType = GetType();
            jsonDataTarget = new TJsonTarget();
            jsonDataTargetType = jsonDataTarget.GetType();
        }

        public bool IsValid(string jsonString) => ReflectionUtils.ValidateJsonStringType(jsonString: jsonString, expectedType: GetType());

        protected TJsonTarget GetResult()
        {
            PostProcess();
            return jsonDataTarget;
        }

        protected virtual void PostProcess()
        {
            ReflectionUtils.InvokeSetEmptyProperties(sourceInstance: this, targetInstance: jsonDataTarget);
        }
    }
}