using System;

namespace NETCoreJsonMapper.Common.Enums
{
    [Flags]
    internal enum PropertyMapType : byte
    {
        VALUE_TO_VALUE = 0,
        VALUE_TO_CLASS = 1 << 0,
        CLASS_TO_VALUE = 1 << 1,
        CLASS_TO_CLASS = 1 << 2
    }
}
