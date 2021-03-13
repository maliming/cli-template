namespace MyCompanyName.MyProjectName.Args
{
    public interface ICommandLineArgumentParser
    {
        CommandLineArgs Parse(string[] args);
    }
}