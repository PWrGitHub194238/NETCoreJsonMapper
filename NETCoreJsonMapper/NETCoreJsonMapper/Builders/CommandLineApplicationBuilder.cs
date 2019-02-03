using Microsoft.Extensions.CommandLineUtils;
using NETCoreJsonMapper.Properties;
using NETCoreJsonMapper.Utils;
using System;
using System.IO;
using System.Linq;

namespace NETCoreJsonMapper.Builders
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
            commandLineApplication.Option(template: CMD_INPUT_TEMPLATE,
                        description: Resources.CMD_INPUT_DESC,
                        optionType: CommandOptionType.MultipleValue);
            commandLineApplication.Option(template: CMD_OUTPUT_TEMPLATE,
                        description: Resources.CMD_OUTPUT_DESC,
                        optionType: CommandOptionType.SingleValue);

            commandLineApplication.OnExecute(() =>
                OnExecuteHandler(commandLineApplication: commandLineApplication));
            commandLineApplication.Execute(args);
        }

        private static int OnExecuteHandler(CommandLineApplication commandLineApplication)
        {
            string validationErrorMsg;
            CommandOption inputOption = commandLineApplication.GetOptions()
                .Where(o => o.Template.Equals(CMD_INPUT_TEMPLATE)).FirstOrDefault();
            CommandOption outputOption = commandLineApplication.GetOptions()
                .Where(o => o.Template.Equals(CMD_OUTPUT_TEMPLATE)).FirstOrDefault();

            if (!ValidateInputCommandOption(inputOption: inputOption, validationMessage: out validationErrorMsg)
                || !ValidateOutputCommandOption(outputOption: outputOption, validationMessage: out validationErrorMsg))
            {
                Console.WriteLine(validationErrorMsg);
                commandLineApplication.ShowHelp();
                return 1;
            }
            Startup.Execute(jsonFilePaths:
                FileUtils.GetJsonDataSourceFileSet(
                    inputDirList: inputOption.Values),
                outputDir: outputOption.Value());
            return 0;
        }

        private static bool ValidateInputCommandOption(CommandOption inputOption, out string validationMessage)
        {
            validationMessage = string.Empty;

            if (inputOption != null && inputOption.HasValue())
            {
                foreach (string inputFullPath in inputOption.Values)
                {
                    if (!Directory.Exists(inputFullPath))
                    {
                        validationMessage += string.Format(Resources.CMD_INPUT_VALIDATE_ERROR_NOT_EXIST, inputFullPath);
                    }
                }
            }
            return validationMessage.Equals(string.Empty);
        }

        private static bool ValidateOutputCommandOption(CommandOption outputOption, out string validationMessage)
        {
            validationMessage = string.Empty;

            if (outputOption == null || !outputOption.HasValue())
            {
                validationMessage = Resources.CMD_OUTPUT_VALIDATE_ERROR_NOT_SET;
            }
            else
            {
                string outputDirectory = outputOption.Value();
                if (!Directory.Exists(outputDirectory))
                {
                    try
                    {
                        Directory.CreateDirectory(outputDirectory);
                    }
                    catch (Exception e)
                    {
                        validationMessage = string.Format(Resources.CMD_OUTPUT_VALIDATE_ERROR_CREATE_DIR,
                            outputDirectory, e.Message);
                    }
                }
            }
            return validationMessage.Equals(string.Empty);
        }
    }
}