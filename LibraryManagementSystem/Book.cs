public class Book
{
    //Suggestions for additional functionalities: multiple people, copies of books, and more data about the book
    //e.g. author and genre and make these into classes with constructors and properties
    //AND PUT THIS ON GIT HUB
    private string title = "";
    private bool checkedOut;
    private string author = "";
    private Genre genre;
    private int copies = 1;
    
    public string Title { get => title; private set => title = value; }
    public bool CheckedOut { get => checkedOut; private set => checkedOut = value; }

    public string Author { get => author; private set => author = value; }
    
    public Genre Genre { get => genre; private set => genre = value; }
    
    public int Copies { get => copies; private set => copies = value; }

    public Book(string newTitle, string newAuthor, Genre newGenre)
    {
        title = newTitle;
        author = newAuthor;
        genre = newGenre;
    }
    public Book(string newTitle, string newAuthor, Genre newGenre, int numCopies)
    {
        title = newTitle;
        author = newAuthor;
        genre = newGenre;
        copies = numCopies;
    }

    //Tom notes: add constructors so that there are private setters
    public static bool operator ==(Book book1, Book book2)
    {
        if (book1 is null && book2 is null)
            return true;
        if (book1 is null || book2 is null)
            return false;
        return book1.Title == book2.Title;
    }

    public static bool operator !=(Book book1, Book book2)
    {
        return !(book1 == book2);
    }
    
    public static void checkOut(Book book)
    {
        book.CheckedOut = true;
    }
    
    public static void checkIn(Book book)
    {
        book.CheckedOut = false;
    }
}