using Serilog;
using Serilog.Core;
using System;

namespace NETCoreJsonMapper.Utils
{
    public static class LogUtils
    {
        public static readonly Logger Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.File($"{DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss")}consoleapp.log")
            .CreateLogger();
    }
}