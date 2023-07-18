using System;
using System.IO;

namespace MyCompanyName.MyProjectName
{
    public static class CliConst
    {
        public static readonly string ConfigDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".MyCompanyName.MyProjectName");

        public static readonly string LogFileName = Path.Combine(ConfigDirectory, "logs", "logs.txt");

        public static readonly string ToolCommandName = "ccs";
    }
}
