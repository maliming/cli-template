using System;
using MyCompanyName.MyProjectName.Args;

namespace MyCompanyName.MyProjectName.Commands
{
    public interface ICommandSelector
    {
        Type Select(CommandLineArgs commandLineArgs);
    }
}
