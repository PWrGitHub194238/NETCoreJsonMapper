using NETCoreJsonMapper.Common.Utils;
using NETCoreJsonMapper.Extension.System;
using NETCoreJsonMapper.Extension.System.Reflection;
using NETCoreJsonMapper.Interface.Mappings;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace NETCoreJsonMapper.Common.Mappings
{
    internal class SourcePropertyMapper<TJsonTarget>
        where TJsonTarget : IJsonDataTarget, new()
    {
        private AJsonDataSource<TJsonTarget> jsonDataSourceInstance;

        public SourcePropertyMapper(AJsonDataSource<TJsonTarget> jsonDataSourceInstance)
        {
            this.jsonDataSourceInstance = jsonDataSourceInstance;
        }

        internal void SetEmptyProperties(TJsonTarget targetInstance, Type sourceType, Type targetType)
        {
            SetEmptyProperties(targetInstance: (object)targetInstance,
                sourceType: sourceType, targetType: targetType);
        }

        private void SetEmptyProperties(object targetInstance, Type sourceType, Type targetType)
        {
            ISet<string> classKeySet = ReflectionUtils.GetClassPropertyOnlyCollection(sourceType);
            foreach (string property in classKeySet)
            {
                PropertyInfo sourceProperty = sourceType.GetPublicInstanceProperty(propertyName: property);
                if (sourceProperty.HasGetter())
                {
                    PropertyInfo targetProperty = targetType.GetPublicInstanceProperty(propertyName: property);
                    if (targetProperty.HasGetter())
                    {
                        SourcePropertySetter<TJsonTarget> jsonDataSourceSetter = new SourcePropertySetter<TJsonTarget>(dataSourceInstance: jsonDataSourceInstance,
                            sourceProperty: sourceProperty, targetInstance: targetInstance, targetProperty: targetProperty);

                        jsonDataSourceSetter.SetEmptyProperty();
                    }
                }
            }
        }
    }
}