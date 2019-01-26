using SitecoreJsonMapper.Builders;

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