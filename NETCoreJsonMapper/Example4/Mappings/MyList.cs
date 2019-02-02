using System.Collections.Generic;
using static Example4.Mappings.JsonDataSource;
using static Example4.Mappings.JsonDataTarget;

namespace Example4.Mappings
{
    public class MyList : List<ItemTargetClass>
    {
        public MyList(List<ItemClass> list)
        {
            foreach (var item in list)
            {
                Add(new ItemTargetClass(item));
            }
        }
    }
}