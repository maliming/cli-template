using MyCompanyName.MyProjectName.Commands;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(AbpAutofacModule)
    )]
    public class MyProjectNameDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<CliOptions>(options =>
            {
                options.Commands["help"] = typeof(HelpCommand);
                options.Commands["version"] = typeof(VersionCommand);

                //add more commands here.
            });
        }
    }
}
