using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Volo.Abp;

namespace MyCompanyName.MyProjectName
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(CliConst.LogFileName)
                .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            using (var application = AbpApplicationFactory.Create<AppModule>(
                options =>
                {
                    options.UseAutofac();
                    options.Services.AddLogging(c => c.AddSerilog());
                }))
            {
                application.Initialize();

                await application.ServiceProvider
                    .GetRequiredService<CliService>()
                    .RunAsync(args);

                application.Shutdown();
            }
        }
    }
}
