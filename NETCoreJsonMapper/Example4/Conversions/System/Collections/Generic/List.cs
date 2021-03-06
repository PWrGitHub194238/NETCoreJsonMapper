﻿using System.Collections.Generic;
using static Example4.Mappings.JsonDataSource;
using static Example4.Mappings.JsonDataTarget;

namespace Example4.Conversions.System.Collections.Generic
{
    public static class List
    {
        public static void Convert(this List<ItemClass> source,
            List<ItemTargetClass> target)
        {
            foreach (ItemClass item in source)
            {
                target.Add(new ItemTargetClass(item));
            }
        }
    }
}