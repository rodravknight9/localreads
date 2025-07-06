using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LocalReads.Shared.DataTransfer.GoogleBooks;

[DataContract(Name = "book")]
public class GoogleBook
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("etag")]
    public string Etag { get; set; }

    [JsonPropertyName("selfLink")]
    public string SelfLink { get; set; }

    [JsonPropertyName("volumeInfo")]
    public VolumeInfo VolumeInfo { get; set; }
}