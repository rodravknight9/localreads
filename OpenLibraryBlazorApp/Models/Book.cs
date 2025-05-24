using System.Text.Json.Serialization;

namespace OpenLibraryBlazorApp.Models
{
    public class Book
    {
        [JsonPropertyName("author_key")]
        public List<string> AuthorKey { get; set; }
        [JsonPropertyName("author_name")]
        public List<string> AuthorName { get; set; }
    }
}
