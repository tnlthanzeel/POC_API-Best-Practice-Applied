using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace VPMS.FuncApp.SourcePathUrlFixer;

public class FixSourcePathsFunc
{
    private readonly string _blobBaseUrl;

    public FixSourcePathsFunc(IConfiguration configuration)
    {
        _blobBaseUrl = configuration.GetValue<string>("BlobBaseUrl");
    }

    [FunctionName("FixSourcePaths")]
    public async Task<IActionResult> FixSourcePaths(
            [HttpTrigger(AuthorizationLevel.Function, Http.Post, Route = null)] HttpRequest req)
    {
        string emailBodyContent = await new StreamReader(req.Body).ReadToEndAsync();

        var model = JsonConvert.DeserializeObject<EmailModel>(emailBodyContent);

        StringBuilder emailBody = new();

        emailBody.Append(model.EmailBody ?? string.Empty);

        FixSrcPathForInlineContent(model, emailBody);

        return new CreatedResult(string.Empty, new { emailBody = emailBody.ToString() });
    }

    private void FixSrcPathForInlineContent(EmailModel model, StringBuilder emailBody)
    {
        var inlineAttachments = model.AttachmentInfo.Where(w => w.IsInline).ToList();

        foreach (var item in inlineAttachments)
        {
            var hasMatchingContentId = model.EmailBody.Contains(item.ContentId);

            if (hasMatchingContentId)
            {
                emailBody.Replace($"cid:{item.ContentId}", $"{_blobBaseUrl}{item.Path}");
            }
        }
    }
}
