using System.Threading.Tasks;
using MyCompanyName.MyProjectName.Args;

namespace MyCompanyName.MyProjectName.Commands
{
    public interface IConsoleCommand
    {
        Task ExecuteAsync(CommandLineArgs commandLineArgs);

        string GetUsageInfo();

        string GetShortDescription();
    }
}
