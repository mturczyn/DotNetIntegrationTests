using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests;

public class UnitTest1 :IClassFixture<WebApplicationFactory<Program>>
{
    private WebApplicationFactory<Program> _factory;

    public UnitTest1(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Test1()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
    }
}