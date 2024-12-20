using System.Net.Http;
using System.Threading.Tasks;

namespace functionapp;

public interface IDependency
{
    Task<string> GetDataFromApi();
}

public class Dependency : IDependency 
{
    private readonly HttpClient _httpClient;

    public Dependency(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetDataFromApi() 
    {
        var response = await _httpClient.GetAsync("https://api.sampleapis.com/codingresources/codingResources");

        var body = await response.Content.ReadAsStringAsync();

        return body;
    }
}
