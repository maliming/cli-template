using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MyCompanyName.MyProjectName.Args;
using Volo.Abp.DependencyInjection;

namespace MyCompanyName.MyProjectName.Commands
{
    public abstract class ConsoleCommandBase<TCommand> : IConsoleCommand, ITransientDependency
    {
        public ILogger<TCommand> Logger { get; set; }

        public ConsoleCommandBase()
        {
            Logger = NullLogger<TCommand>.Instance;
        }

        public abstract Task ExecuteAsync(CommandLineArgs commandLineArgs);

        public abstract string GetUsageInfo();

        public abstract string GetShortDescription();
    }
}
