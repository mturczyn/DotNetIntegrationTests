using Microsoft.Azure.Functions.Extensions.DependencyInjection;

namespace functionapp.tests.TestsSetup;

public class TestsStartup : Startup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        base.Configure(builder);
    }
}
