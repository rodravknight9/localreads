using LocalReads.DTO;

namespace LocalReads.State;

public class SearchResults
{
    public List<BookDto> Books { get; set; } = new List<BookDto>();
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

    public void SetSearchResults(List<BookDto> books)
    {
        Books = books;
        NotifyStateChanged();
    }
    private void NotifyStateChanged() => OnChange?.Invoke();
}