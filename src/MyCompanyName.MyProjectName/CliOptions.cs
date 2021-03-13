using System;
using System.Collections.Generic;

namespace MyCompanyName.MyProjectName
{
    public class CliOptions
    {
        public Dictionary<string, Type> Commands { get; }

        public CliOptions()
        {
            Commands = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
        }
    }
}
