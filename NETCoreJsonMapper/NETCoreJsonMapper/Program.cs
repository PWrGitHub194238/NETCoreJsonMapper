using NETCoreJsonMapper.Builders;

namespace NETCoreJsonMapper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CommandLineApplicationBuilder.ParseCmdLneArguments(args);
        }
    }
}