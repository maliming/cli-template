using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyCompanyName.MyProjectName.Args;

namespace MyCompanyName.MyProjectName.Commands;

public class ChangeCommand : ConsoleCommandBase<VersionCommand>
{
    public override Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var appsettings = Directory.GetFiles(currentDirectory, "appsettings.json", SearchOption.AllDirectories);
        if (appsettings.Length == 0)
        {
            Logger.LogWarning($"There is no appsettings.json file in {currentDirectory}.");
            return Task.CompletedTask;
        }

        foreach (var appsetting in appsettings.Where(x => !x.Contains("bin/Debug") && !x.Contains("bin/Release")))
        {
            var content = File.ReadAllText(appsetting);

            var changed = false;
            if (content.Contains("Trusted_Connection=True"))
            {
                content = content.Replace("Trusted_Connection=True", "User Id=sa;Password=1q2w3E***");
                changed = true;
            }
            else if (content.Contains("User Id=sa;Password=1q2w3E***"))
            {
                content = content.Replace("User Id=sa;Password=1q2w3E***", "Trusted_Connection=True");
                changed = true;
            }

            if (content.Contains(@"(LocalDb)\\MSSQLLocalDB"))
            {
                content = content.Replace(@"(LocalDb)\\MSSQLLocalDB", "localhost");
                changed = true;
            }

            if (changed)
            {
                Logger.LogInformation($"Change connection string in {appsetting}");
            }

            File.WriteAllText(appsetting, content);
        }

        return Task.CompletedTask;
    }

    public override string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("Usage:");
        sb.AppendLine($"\t{CliConst.ToolCommandName} change");

        return sb.ToString();
    }

    public override string GetShortDescription()
    {
        return "Change connection string.";
    }
}
