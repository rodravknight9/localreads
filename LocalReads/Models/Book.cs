using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace LocalReads.Models;

public class Root
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; }
    [JsonPropertyName("totalItems")]
    public int TotalItems { get; set; }
    [JsonPropertyName("items")]
    public List<Book> Items { get; set; }
}

public class Book
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

public class VolumeInfo
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("authors")]
    public List<string> Authors { get; set; }

    [JsonPropertyName("publisher")]
    public string Publisher { get; set; }

    [JsonPropertyName("publishedDate")]
    public string PublishedDate { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("industryIdentifiers")]
    public List<IndustryIdentifier> IndustryIdentifiers { get; set; }

    [JsonPropertyName("pageCount")]
    public int PageCount { get; set; }

    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; }

    [JsonPropertyName("maturityRating")]
    public string MaturityRating { get; set; }

    [JsonPropertyName("contentVersion")]
    public string ContentVersion { get; set; }

    [JsonPropertyName("imageLinks")]
    public ImageLinks ImageLinks { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; }

    [JsonPropertyName("previewLink")]
    public string PreviewLink { get; set; }

    [JsonPropertyName("infoLink")]
    public string InfoLink { get; set; }

    [JsonPropertyName("canonicalVolumeLink")]
    public string CanonicalVolumeLink { get; set; }
}

public class IndustryIdentifier
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }
}
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