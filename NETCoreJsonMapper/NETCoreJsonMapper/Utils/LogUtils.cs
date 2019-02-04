using Serilog;
using Serilog.Core;
using System;

namespace NETCoreJsonMapper.Utils
{
    internal static class LogUtils
    {
        internal static readonly Logger logger = new LoggerConfiguration()
            .WriteTo.File($"{DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss")}consoleapp.log")
            .CreateLogger();
    }
}