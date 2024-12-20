using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace functionapp;

public class Function1
{
    [FunctionName("Function1")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, IDependency dependency)
    {
        string name = req.Query["name"];

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        string resultFromDependency = await dependency.GetDataFromApi();
        
        string responseMessage = $"name from query '{name}', request body '{requestBody}'. Result from dependency '{resultFromDependency}'";

        return new OkObjectResult(responseMessage);
    }
}
