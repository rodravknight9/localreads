using System.Text.Json.Serialization;

namespace LocalReads.Shared.DataTransfer.GoogleBooks;

public class IndustryIdentifier
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }
}
