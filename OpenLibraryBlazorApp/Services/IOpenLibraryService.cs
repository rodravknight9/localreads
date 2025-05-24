namespace OpenLibraryBlazorApp.Services
{
    public interface IOpenLibraryService
    {
        public Task GetRandomBooks();

        //https://openlibrary.org/search.json?q=the&limit=5&offset=1&fields=author_key,author_name,first_publish_year,key,language,title,isbn
        //https://covers.openlibrary.org/b/isbn/1986537536.jpg
    }
}
