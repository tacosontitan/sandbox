using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Sandbox.Core;
using Sandbox.Modules;
using Sandbox.Terminal;

Console.WriteLine("Starting sandbox.");
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(ConfigureAppConfiguration)
    .ConfigureLogging(ConfigureLogging)
    .ConfigureServices(Configure)
    .Build();

host.Run();

void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder config)
{
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true);
    config.AddEnvironmentVariables();
    if (args != null)
        config.AddCommandLine(args);
}

void ConfigureLogging(HostBuilderContext context, ILoggingBuilder logging)
{
    logging.ClearProviders();
    logging.AddDebug();

    IConfigurationSection loggingConfiguration = context.Configuration.GetSection("Logging");
    if (loggingConfiguration is null)
        return;

    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
}

void Configure(IServiceCollection services)
{
    services
        .AddModuleDependencies()
        .AddModulesFromAssemblyContaining<Module>()
        .AddModulesFromAssemblyContaining<HelloWorld>()
        .AddModulesFromAssemblyContaining<Program>()
        .AddTerminal();
}
