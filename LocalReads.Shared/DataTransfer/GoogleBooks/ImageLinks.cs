using System.Text.Json.Serialization;

namespace LocalReads.Shared.DataTransfer.GoogleBooks;
public class ImageLinks
{
    [JsonPropertyName("smallThumbnail")]
    public string SmallThumbnail { get; set; }

    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; set; }

    [JsonPropertyName("small")]
    public string Small { get; set; }

    [JsonPropertyName("medium")]
    public string Medium { get; set; }

    [JsonPropertyName("large")]
    public string Large { get; set; }

    [JsonPropertyName("extraLarge")]
    public string ExtraLarge { get; set; }

}