using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace VPMS.FuncApp.SaveToRepository;

public class SaveEmailReferenceFunc
{
    private readonly string _dbConnection;

    public SaveEmailReferenceFunc(IConfiguration configuration)
    {
        _dbConnection = configuration.GetConnectionString("DbConnection");
    }

    [FunctionName("SaveEmailReferencesToDB")]
    public async Task<IActionResult> SaveEmailReferencesToDB(
            [HttpTrigger(AuthorizationLevel.Function, Http.Post, Route = null)] HttpRequest req)
    {

        string emailBodyContent = await new StreamReader(req.Body).ReadToEndAsync();

        var model = JsonConvert.DeserializeObject<SaveEmailRefrenceToDbModel>(emailBodyContent);

        //write code to save the email references to the required type of database

        return new OkResult();
    }
}
