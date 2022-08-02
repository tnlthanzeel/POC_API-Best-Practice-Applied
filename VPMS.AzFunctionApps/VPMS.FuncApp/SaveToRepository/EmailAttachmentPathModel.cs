using System.Text.Json.Serialization;

namespace VPMS.FuncApp.SaveToRepository;

internal class EmailAttachmentPathModel
{
    [JsonPropertyName("contentId")]
    public string ContentId { get; set; }

    [JsonPropertyName("path")]
    public string Path { get; set; }
}


