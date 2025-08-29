public class LibraryCard
{
    private string cardHolderName;
    private Guid cardNumber = Guid.Empty;
    private static readonly int MaxBorrowLimit = 3;
    private int borrowCount;
    private static List<Book> borrowedBooks = new();
    private static int BorrowedCount => borrowedBooks.Count(s => s.CheckedOut);
    private static bool BorrowLimitHit => BorrowedCount >= MaxBorrowLimit;
    
    public string CardHolderName { get => cardHolderName; private set => cardHolderName = value; }
    public int CardNumber {
        get
        {
            if(cardNumber == Guid.Empty)
                cardNumber = GenerateCardNumber();
            return cardNumber.GetHashCode();
        }
    }
    public int BorrowCount { get => borrowCount; private set => borrowCount = value; }
    
    public LibraryCard(string newCardHolderName)
    {
        cardHolderName = newCardHolderName;
    }
    
    public static Guid GenerateCardNumber()
    {
        return Guid.NewGuid();
    }
}

public class LibraryCardTracker
{
    private static List<LibraryCard> libraryCards = new(); 
    
    public static void CreateLibraryCard(string cardHolderName)
    {
        libraryCards.Add(new LibraryCard(cardHolderName));
    }
}