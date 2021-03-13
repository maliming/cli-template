using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using MyCompanyName.MyProjectName.Args;
using Volo.Abp.DependencyInjection;

namespace MyCompanyName.MyProjectName.Commands
{
    public class HelpCommand : IConsoleCommand, ITransientDependency
    {
        public ILogger<HelpCommand> Logger { get; set; }
        protected CliOptions CliOptions { get; }
        protected IServiceScopeFactory ServiceScopeFactory { get; }

        public HelpCommand(IOptions<CliOptions> cliOptions,
            IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
            Logger = NullLogger<HelpCommand>.Instance;
            CliOptions = cliOptions.Value;
        }

        public Task ExecuteAsync(CommandLineArgs commandLineArgs)
        {
            if (string.IsNullOrWhiteSpace(commandLineArgs.Target))
            {
                Logger.LogInformation(GetUsageInfo());
                return Task.CompletedTask;
            }

            if (!CliOptions.Commands.ContainsKey(commandLineArgs.Target))
            {
                Logger.LogWarning($"There is no command named {commandLineArgs.Target}.");
                Logger.LogInformation(GetUsageInfo());
                return Task.CompletedTask;
            }

            var commandType = CliOptions.Commands[commandLineArgs.Target];

            using (var scope = ServiceScopeFactory.CreateScope())
            {
                var command = (IConsoleCommand) scope.ServiceProvider.GetRequiredService(commandType);
                Logger.LogInformation(command.GetUsageInfo());
            }

            return Task.CompletedTask;
        }

        public string GetUsageInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine("Usage:");
            sb.AppendLine($"\t{CliConst.ToolCommandName} <command> <target> [options]");
            sb.AppendLine();
            sb.AppendLine("Command List:");

            foreach (var command in CliOptions.Commands.ToArray())
            {
                string shortDescription;

                using (var scope = ServiceScopeFactory.CreateScope())
                {
                    shortDescription = ((IConsoleCommand) scope.ServiceProvider
                            .GetRequiredService(command.Value)).GetShortDescription();
                }

                sb.Append("\t> ");
                sb.Append(command.Key);
                sb.Append(string.IsNullOrWhiteSpace(shortDescription) ? "" : ": ");
                sb.AppendLine(shortDescription);
            }

            sb.AppendLine();
            sb.AppendLine("More info: https://github.com/maliming/cli-template");

            return sb.ToString();
        }

        public string GetShortDescription()
        {
            return $"Show command line help. Write `{CliConst.ToolCommandName} help <command>`";
        }
    }
}
