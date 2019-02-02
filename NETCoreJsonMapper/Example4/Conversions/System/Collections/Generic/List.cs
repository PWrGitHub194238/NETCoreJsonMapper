using Example4.Mappings;
using System.Collections.Generic;
using static Example4.Mappings.JsonDataSource;
using static Example4.Mappings.JsonDataTarget;

namespace Example4.Conversions.System.Collections.Generic
{
    public static class Extensions
    {
        public static void MoveToTop(this List<ItemTargetClass> target, List<ItemClass> source)
        {
            foreach (var item in source)
            {
                target.Add(new ItemTargetClass(item));
            }
        }

        public static void Foo(this string x) { }
        public static void Bar(string x) { } // Not an ext. method
        public static void Baz(this int x) { } // Not on string
    }
}