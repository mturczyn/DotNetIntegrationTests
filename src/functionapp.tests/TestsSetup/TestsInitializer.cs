using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;

namespace functionapp.tests.TestsSetup;

public class TestsInitializer
{
    public TestsInitializer()
    {
        var builder = new HostBuilder()
           .ConfigureWebJobs(configBuilder =>
           {
               configBuilder.UseWebJobsStartup(typeof(TestsStartup), new WebJobsBuilderContext(), NullLoggerFactory.Instance);
           });

        IHost host = builder.Build();

        ServiceProvider = host.Services;
    }

    public IServiceProvider ServiceProvider { get; }
}
