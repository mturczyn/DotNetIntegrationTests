
using functionapp.tests.TestsSetup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;

namespace functionapp.tests;

[Collection(IntegrationTestsCollection.Name)]
public class UnitTest1 : IClassFixture<TestsStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly Function1 _sut;
    private readonly IDependency _dependency;

    public UnitTest1(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = new Function1();
        _dependency = testsInitializer.ServiceProvider.GetRequiredService<IDependency>();
    }

    [Fact]
    public async Task Test1()
    {
        // Arragne
        var queryName = "value";
        var bodyName = "body value";
        var request = GetRequest(queryName, bodyName);

        // Act
        var response = await _sut.Run(request, _dependency);

        // Assert
        Assert.IsType<OkObjectResult>(response);
        var responseValue = (response as OkObjectResult).Value.ToString();
        Assert.Contains($"'{queryName}'", responseValue);
        Assert.Contains($"{bodyName}", responseValue);
    }

    private static HttpRequest GetRequest(string queryNameValue, string bodyNameValue)
    {
        var request = new DefaultHttpRequest(new DefaultHttpContext());
        request.Query = new QueryCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>()
        {
            { "name", queryNameValue }
        });
        request.Body = GetStream(bodyNameValue);
        return request;
    }

    private static Stream GetStream(string nameValue)
    {
        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(new { name = nameValue })));
        return memoryStream;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }
}