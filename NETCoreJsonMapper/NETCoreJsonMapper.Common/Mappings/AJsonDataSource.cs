using NETCoreJsonMapper.Common.Utils;
using NETCoreJsonMapper.Interface.Mappings;
using System;

namespace NETCoreJsonMapper.Common.Mappings
{
    public abstract class AJsonDataSource<TJsonTarget> : IJsonDataSource<TJsonTarget>
        where TJsonTarget : IJsonDataTarget, new()
    {
        private readonly Type jsonDataSourceType;
        private readonly Type jsonDataTargetType;

        protected TJsonTarget jsonDataTarget;

        public AJsonDataSource()
        {
            jsonDataSourceType = GetType();
            jsonDataTarget = new TJsonTarget();
            jsonDataTargetType = jsonDataTarget.GetType();
        }

        protected virtual void PostProcess() => ReflectionUtils.SetEmptyProperties(sourceInstance: this, targetInstance: jsonDataTarget,
                sourceType: jsonDataSourceType, targetType: jsonDataTargetType);

        public bool IsValid(string jsonString) => ReflectionUtils.ValidateJsonStringType(jsonString: jsonString, expectedType: GetType());

        protected TJsonTarget GetResult()
        {
            PostProcess();
            return jsonDataTarget;
        }
    }
}
