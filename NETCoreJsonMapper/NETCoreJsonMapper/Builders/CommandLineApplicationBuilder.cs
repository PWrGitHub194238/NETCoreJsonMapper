using Microsoft.Extensions.CommandLineUtils;
using NETCoreJsonMapper.Loggers.Utils;
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
        private const int COM_LINE_VALIDATION_ERROR_RETURN_CODE = 1;
        private const int CMD_LINE_VALIDATIN_OK_RETURN_CODE = 0;

        internal static void ParseCmdLneArguments(string[] args)
        {
            CommandLineApplication commandLineApplication = BuildCommandLineWithOptions();

            commandLineApplication.OnExecute(() =>
                OnExecuteHandler(commandLineApplication: commandLineApplication));
            commandLineApplication.Execute(args);
        }

        private static CommandLineApplication BuildCommandLineWithOptions()
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

            return commandLineApplication;
        }

        private static int OnExecuteHandler(CommandLineApplication commandLineApplication)
        {
            int validationRetunCode = CMD_LINE_VALIDATIN_OK_RETURN_CODE;

            CommandOption inputOption = commandLineApplication.GetOptions()
                .Where(o => o.Template.Equals(CMD_INPUT_TEMPLATE)).FirstOrDefault();
            CommandOption outputOption = commandLineApplication.GetOptions()
                .Where(o => o.Template.Equals(CMD_OUTPUT_TEMPLATE)).FirstOrDefault();

            DefaultLogger.Verbose(Resources.LOG_VERBOSE_CMD_LINE_OPTIONS, inputOption, outputOption);

            if (!ValidateInputCommandOption(inputOption: inputOption, validationMessage: out string validationErrorMsg)
                || !ValidateOutputCommandOption(outputOption: outputOption, validationMessage: out validationErrorMsg))
            {
                DefaultLogger.Verbose(Resources.LOG_VERBOSE_CMD_LINE_VALIDATION_SUMMARY, validationErrorMsg);
                validationRetunCode = COM_LINE_VALIDATION_ERROR_RETURN_CODE;
                Console.WriteLine(validationErrorMsg);
                commandLineApplication.ShowHelp();
            }
            else
            {
                Startup.Execute(jsonFilePaths:
                    FileUtils.GetJsonDataSourceFileSet(
                        inputDirList: inputOption.Values),
                    outputDir: outputOption.Value());
            }

            return validationRetunCode;
        }

        private static bool ValidateInputCommandOption(CommandOption inputOption, out string validationMessage)
        {
            DefaultLogger.Verbose(Resources.LOG_VERBOSE_CMD_LINE_INPUT_OPTION_VALIDATION, inputOption);
            validationMessage = string.Empty;

            if (inputOption != null && inputOption.HasValue())
            {
                foreach (string inputFullPath in inputOption.Values)
                {
                    DefaultLogger.Verbose(Resources.LOG_VERBOSE_CMD_LINE_INPUT_OPTION_PARAM_VALIDATION, inputFullPath);
                    if (!Directory.Exists(inputFullPath))
                    {
                        validationMessage += string.Format(Resources.CMD_INPUT_VALIDATE_ERROR_NOT_EXIST, inputFullPath);
                    }
                }
            }
            else
            {
                validationMessage += string.Format(Resources.CMD_INPUT_VALIDATE_NO_INPUT);
            }
            return validationMessage.Equals(string.Empty);
        }

        private static bool ValidateOutputCommandOption(CommandOption outputOption, out string validationMessage)
        {
            DefaultLogger.Verbose(Resources.LOG_VERBOSE_CMD_LINE_OUTPUT_OPTION_VALIDATION, outputOption);

            validationMessage = string.Empty;

            if (outputOption != null && outputOption.HasValue())
            {
                string outputDirectory = outputOption.Value();
                if (!Directory.Exists(outputDirectory))
                {
                    try
                    {
                        DefaultLogger.Verbose(Resources.LOG_VERBOSE_CMD_LINE_OUTPUT_NOT_EXISTS_VALIDATION, outputDirectory);
                        Directory.CreateDirectory(outputDirectory);
                    }
                    catch (Exception e)
                    {
                        validationMessage = string.Format(Resources.CMD_OUTPUT_VALIDATE_ERROR_CREATE_DIR,
                            outputDirectory, e.Message);
                    }
                }
            }
            else
            {
                validationMessage += string.Format(Resources.CMD_OUTPUT_VALIDATE_ERROR_NOT_SET);
            }

            return validationMessage.Equals(string.Empty);
        }
    }
}