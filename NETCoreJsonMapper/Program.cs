using Newtonsoft.Json;
using SitecoreJsonMapper.Builders;
using System;
using System.Collections.Generic;
using System.IO;

namespace SitecoreJsonMapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CommandLineApplicationBuilder.ParseCmdLneArguments(args);
        }
    }
}