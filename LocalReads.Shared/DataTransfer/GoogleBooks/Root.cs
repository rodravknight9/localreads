using System.Text.Json.Serialization;

namespace LocalReads.Shared.DataTransfer.GoogleBooks;

public class Root
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; }
    [JsonPropertyName("totalItems")]
    public int TotalItems { get; set; }
    [JsonPropertyName("items")]
    public List<GoogleBook> Items { get; set; }
}


