using LocalReads.DTO;

namespace LocalReads.State;

public class SearchResults
{
    private List<BookDto> _books;
    public List<BookDto> Books 
    { 
        get => _books;
        set
        {
            _books = value;
            NotifySearchResultsChanged();
        }
    } 



    private string _termSearch;
    public string TermSearch { 
        get 
        {
            return _termSearch;
        } 
        set 
        {
            _termSearch = value;
            NotifyStateChanged();
        } 
    }
    public event Action? OnChange;

    public event Action? OnBooksSearchChanged;
    public void SetSearchResults(List<BookDto> books)
    {
        //NotifySearchResultsChanged();
        Books = books;
        NotifyStateChanged();
    }
    private void NotifyStateChanged() => OnChange?.Invoke();
    private void NotifySearchResultsChanged() => OnBooksSearchChanged?.Invoke();

}