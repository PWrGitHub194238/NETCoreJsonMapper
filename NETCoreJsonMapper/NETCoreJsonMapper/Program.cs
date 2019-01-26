using Newtonsoft.Json;
using NETCoreJsonMapper.Builders;
using System;
using System.Collections.Generic;
using System.IO;

namespace NETCoreJsonMapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CommandLineApplicationBuilder.ParseCmdLneArguments(args);
        }
    }
}