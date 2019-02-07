using NETCoreJsonMapper.Commons.Utils;
using NETCoreJsonMapper.Extensions.System;
using NETCoreJsonMapper.Extensions.System.Reflection;
using System.Collections.Generic;
using System.Reflection;

namespace NETCoreJsonMapper.Commons.Mappings
{
    internal class SourcePropertyMapper<TJsonSource, TJsonTarget>
    {
        private TJsonSource jsonDataSourceInstance;

        public SourcePropertyMapper(TJsonSource jsonDataSourceInstance)
        {
            this.jsonDataSourceInstance = jsonDataSourceInstance;
        }

        internal void SetEmptyProperties(TJsonTarget targetInstance)
        {
            ISet<string> classKeySet = ReflectionUtils.GetClassPropertyOnlyCollection(typeof(TJsonSource));
            foreach (string property in classKeySet)
            {
                PropertyInfo sourceProperty = typeof(TJsonSource).GetPublicInstanceProperty(propertyName: property);
                if (sourceProperty.HasGetter())
                {
                    PropertyInfo targetProperty = typeof(TJsonTarget).GetPublicInstanceProperty(propertyName: property);
                    if (targetProperty.HasGetter())
                    {
                        SourcePropertySetter<TJsonSource, TJsonTarget> jsonDataSourceSetter = new SourcePropertySetter<TJsonSource, TJsonTarget>(dataSourceInstance: jsonDataSourceInstance,
                            sourceProperty: sourceProperty, targetInstance: targetInstance, targetProperty: targetProperty);

                        jsonDataSourceSetter.SetEmptyProperty();
                    }
                }
            }
        }
    }
}