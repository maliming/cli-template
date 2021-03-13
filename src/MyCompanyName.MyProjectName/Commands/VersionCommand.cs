using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyCompanyName.MyProjectName.Args;

namespace MyCompanyName.MyProjectName.Commands
{
    public class VersionCommand : ConsoleCommandBase<VersionCommand>
    {
        public override Task ExecuteAsync(CommandLineArgs commandLineArgs)
        {
            Logger.LogInformation(typeof(VersionCommand).Assembly.GetFileVersion());
            return Task.CompletedTask;
        }

        public override string GetUsageInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Usage:");
            sb.AppendLine($"\t{CliConst.ToolCommandName} version");

            return sb.ToString();
        }

        public override string GetShortDescription()
        {
            return "Get version.";
        }
    }
}
