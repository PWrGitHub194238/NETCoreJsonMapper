using System.Collections.Generic;
using static Example4.Mappings.JsonDataSource;
using static Example4.Mappings.JsonDataTarget;

namespace Example4.Wrappers.System.Collections.Generic
{
    public class MyList : List<ItemTargetClass>
    {
        public MyList(List<ItemClass> list)
        {
            foreach (ItemClass item in list)
            {
                Add(new ItemTargetClass(item));
            }
        }
    }
}