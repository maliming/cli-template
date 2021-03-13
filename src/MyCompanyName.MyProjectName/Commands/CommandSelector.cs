using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MyCompanyName.MyProjectName.Args;
using Volo.Abp.DependencyInjection;

namespace MyCompanyName.MyProjectName.Commands
{
    public class CommandSelector : ICommandSelector, ITransientDependency
    {
        protected CliOptions Options { get; }

        public CommandSelector(IOptions<CliOptions> options)
        {
            Options = options.Value;
        }

        public Type Select(CommandLineArgs commandLineArgs)
        {
            if (commandLineArgs.Command.IsNullOrWhiteSpace())
            {
                return typeof(HelpCommand);
            }

            return Options.Commands.GetOrDefault(commandLineArgs.Command) ?? typeof(HelpCommand);
        }
    }
}
