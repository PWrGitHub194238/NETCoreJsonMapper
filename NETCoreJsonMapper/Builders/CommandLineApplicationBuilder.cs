using Microsoft.Extensions.CommandLineUtils;
using SitecoreJsonMapper.Utils;
using System;
using NETCoreJsonMapper.Properties;

namespace SitecoreJsonMapper.Builders
{
    internal static class CommandLineApplicationBuilder
    {
        private const string CMD_APP_NAME = "NETCoreJsonMapper";

        private const string CMD_HELP_TEMPLATE = "-?|-h|--help";
        private const string CMD_INPUT_TEMPLATE = "-i | --input-dir";
        private const string CMD_OUTPUT_TEMPLATE = "-o | --output-dir";

        internal static void ParseCmdLneArguments(string[] args)
        {
            CommandLineApplication commandLineApplication = new CommandLineApplication(throwOnUnexpectedArg: true)
            {
                Name = CMD_APP_NAME,
                Description = Resources.CMD_APP_DESC
            };
            commandLineApplication.HelpOption(CMD_HELP_TEMPLATE);

            commandLineApplication.OnExecute(() =>
                OnExecuteHandler(inputOption: commandLineApplication.Option(template: CMD_INPUT_TEMPLATE,
                        description: Resources.CMD_INPUT_DESC,
                        optionType: CommandOptionType.MultipleValue),
                    outputOption: commandLineApplication.Option(template: CMD_OUTPUT_TEMPLATE,
                        description: Resources.CMD_OUTPUT_DESC,
                        optionType: CommandOptionType.SingleValue)));
            commandLineApplication.Execute(args);
        }

        private static int OnExecuteHandler(CommandOption inputOption, CommandOption outputOption)
        {
            if (!inputOption.HasValue() || !outputOption.HasValue())
            {
                Console.WriteLine("WRONG!");
                return 1;
            }
            Startup.Execute(jsonFilePaths:
                FileUtils.GetJsonDataSourceFileSet(
                    inputDirList: inputOption.Values),
                outputDir: outputOption.Value());
            return 0;
        }
    }
}
