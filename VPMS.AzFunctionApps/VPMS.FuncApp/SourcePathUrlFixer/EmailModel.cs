using System.Text.Json.Serialization;

namespace VPMS.FuncApp.SourcePathUrlFixer;

internal class EmailModel
{
    [JsonPropertyName("attachmentInfo")]
    public List<AttachmentModel> AttachmentInfo { get; set; }

    [JsonPropertyName("emailBody")]
    public string EmailBody { get; set; }
}
