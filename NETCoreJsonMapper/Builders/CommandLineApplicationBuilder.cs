using Microsoft.Extensions.CommandLineUtils;
using SitecoreJsonMapper.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SitecoreJsonMapper.Builders
{
    internal static class CommandLineApplicationBuilder
    {
        internal static void ParseCmdLneArguments(string[] args)
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: true)
            {
                Name = "ConsoleArgs",
                Description = ".NET Core console app with argument parsing."
            };
            app.HelpOption("-?|-h|--help");
            CommandOption inputDir = app.Option("-i | --input-dir", "", CommandOptionType.MultipleValue);
            CommandOption outputDir = app.Option("-o | --output-dir", "", CommandOptionType.SingleValue);
            app.OnExecute(() => OnExecuteHandler(inputDir, outputDir));
            app.Execute(args);
        }

        private static int OnExecuteHandler(CommandOption inputDir, CommandOption outputDir)
        {
            if (!inputDir.HasValue() || !outputDir.HasValue())
            {
                Console.WriteLine("WRONG!");
                return 1;
            }
            Startup.Execute(jsonFilePaths:
                FileUtils.GetJsonDataSourceFileSet(
                    inputDirList: inputDir.Values),
                outputDir: outputDir.Value());
            return 0;
        }
    }
}
