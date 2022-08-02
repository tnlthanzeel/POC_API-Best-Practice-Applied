using System.Text.Json.Serialization;

namespace VPMS.FuncApp.SaveToRepository;

internal class SaveEmailRefrenceToDbModel
{
    [JsonPropertyName("emailAttachmentPaths")]
    public List<EmailAttachmentPathModel> EmailAttachmentPaths { get; set; }

    [JsonPropertyName("emailContentPath")]
    public string EmailContentPath { get; set; }

    [JsonPropertyName("emailId")]
    public string EmailId { get; set; }
}


