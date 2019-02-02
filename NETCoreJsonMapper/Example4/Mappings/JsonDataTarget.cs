using Example4.Conversions.System.Collections.Generic;
using NETCoreJsonMapper.Interface.Mappings;
using Newtonsoft.Json;
using System.Collections.Generic;
using static Example4.Mappings.JsonDataSource;

namespace Example4.Mappings
{
    /// <summary>
    /// A sample class that represents a JSON file to be generated out of this class' instance.
    /// Along with this class, a JsonDataSource was generated.
    /// 
    /// Example string that will be generated ased on this class:
    /// 
    /// {
    /// 	"Example-Property": "ExampleValue-generated",
    /// }
    /// 
    /// Each of the resulted JSON keys has a value 
    /// of the public property of this class enhanced by the proper JSON attribute.
    /// A data source for all the values generated is a JsonDataSource class.
    /// </summary>
    public class JsonDataTarget : IJsonDataTarget
    {
        public double ExampleProperty { get; set; }

        public string ExampleProperty2 { get; set; }

        public TestClass ExampleProperty3 { get; set; }

        public class TestClass
        {
            public TestClass()
            {
                MyProperty = 2;
            }
            public int MyProperty { get; set; }
        }

        public TestClass1 ExampleProperty4 { get; set; }

        public class TestClass1
        {
            public TestClass1(long i)
            {
                MyProperty = (int)i;
            }
            public int MyProperty { get; set; }
        }

        public List<int> ExampleProperty5 { get; set; }

        public MyList ExampleProperty6 { get; set; }

        public class ItemTargetClass
        {
            public ItemTargetClass(ItemClass c)
            {
                MyProperty = c.MyProperty;
            }

            public int MyProperty { get; set; }
        }

        public List<ItemTargetClass> ExampleProperty7 { get; set; }
    }
}
