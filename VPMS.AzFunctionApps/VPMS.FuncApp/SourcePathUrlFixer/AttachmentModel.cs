using System.Text.Json.Serialization;

namespace VPMS.FuncApp.SourcePathUrlFixer;

internal class AttachmentModel
{
    [JsonPropertyName("path")]
    public string Path { get; set; }

    [JsonPropertyName("contentId")]
    public string ContentId { get; set; }

    [JsonPropertyName("isInline")]
    public bool IsInline { get; set; }
}
