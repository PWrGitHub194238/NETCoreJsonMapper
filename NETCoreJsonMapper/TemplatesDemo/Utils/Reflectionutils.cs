using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using TemplatesDemo.Attributes.SC9.ItemStructure.Page.PageInfo.Template;
using TemplatesDemo.Conversions.SC9.ItemStructure.Page.PageInfo.Template;
using TemplatesDemo.Enums.SC9.ItemStructure.Page.PageInfo.Template;
using static TemplatesDemo.Mappings.JsonDataSource;
using static TemplatesDemo.Mappings.JsonDataTarget;

namespace TemplatesDemo.Utils
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class MyAttribute : Attribute
    {
        //...
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class YourAttribute : Attribute
    {
        //...
    }

    [My]
    public class MyClass
    {

        public virtual void MyMethod()
        {
            //...
        }
    }

    [Your]
    public class YourClass : MyClass
    {
        // MyMethod will have MyAttribute but not YourAttribute.


        public override void MyMethod()
        {
            //...
        }

    }


    class ReflectionUtils
    {
        internal static void GetAttrBasedConstructor(ItemTargetClass instance,
            Type target, TemplateSourceClass source)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => target.IsAssignableFrom(p) && p.BaseType.Equals(target))
    .ToList();

            foreach (var type in types)
            {
                TemplateKeyFieldAttribute a1 = type.GetCustomAttribute<TemplateKeyFieldAttribute>(true);
                var value = source.GetType().GetProperty(a1.KeyField).GetValue(source, null);
                ////var a1d = a1.
                TemplateKeyValueAttribute a2 = type.GetCustomAttribute<TemplateKeyValueAttribute>(true);
                TemplateKeyValueEnum e = a2.KeyValue;
                string value2 = e.GetAttributeOfType<DescriptionAttribute>().Description;

                if (value.Equals(value2))
                {
                    instance.GetType()
                        .GetProperty("Template")
                        .SetValue(instance,
                        Activator.CreateInstance(type,
                            new object[] { source }),
                        null);
                    return;
                }
            }
            //var a1 = target.GetCustomAttribute(typeof(TemplateKeyFieldAttribute), true);
            ////var a1d = a1.
            //var a2 = target.GetCustomAttribute(typeof(TemplateKeyValueAttribute), true);
            //a2 = a2;


            //var a = typeof(YourClass).GetCustomAttributes(true);
        }




    }


    public static class EnumHelper
    {
        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example>string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;</example>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
    }
}
