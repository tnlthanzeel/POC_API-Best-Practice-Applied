using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;

namespace VPMS.FuncApp.VendorEmailValidator;

public class ValidateVendorEmailFunc
{
    [FunctionName("ValidateVendorEmail")]
    public async Task<IActionResult> ValidateVendorEmail(
        [HttpTrigger(AuthorizationLevel.Function, Http.Post, Route = null)] HttpRequest req)
    {

        string vendorEmail = await new StreamReader(req.Body).ReadToEndAsync();

        return new OkObjectResult(new { isValidVendorEmail = true });
    }
}
