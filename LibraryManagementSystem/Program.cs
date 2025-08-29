public class LibraryManagementSystem
{
    // 1. Create Variables for Books: Set up five string variables to store the book titles.
    private static List<Book> books = new();

    private static readonly int MaxBorrowLimit = 3;
    private static int BorrowedCount => books.Count(s => s.CheckedOut);
    private static bool BorrowLimitHit => BorrowedCount >= MaxBorrowLimit;
    private static bool NoBooksBorrowed => books.All(s => !s.CheckedOut);
    
    // 5. Loop Indefinitely: Continuously prompt the user to choose whether they want to add or remove a book, or exit the program. If the user chooses to exit, break the loop and end the program.
    // 6. Handle Invalid Inputs: If the user enters an invalid action (neither "add" nor "remove"), inform them and prompt again.
    
    public static void Main(string[] args)
    {
        bool continueRunning = true;
        
        while (continueRunning)
        {
            Console.WriteLine(
                "Welcome to the library! Would you like to: \n 1. Add a book \n 2. Remove a book \n 3. Display what books are in the library \n 4. Search for a book in the library \n 5. Check in a borrowed book \n 6. Exit the library");
            string input = Console.ReadLine();

            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please input a valid command: Add, Remove, Display, Search, Check In, or Exit\n");
                input = Console.ReadLine();
            }

            if (input.ToLower() == "add" || input == "1")
            {
                // 7. Conditional Actions: Only allow adding books if there are empty slots.
                if (books.Count < 5)
                    AddBook();
                else
                {
                    Console.WriteLine("You can only have 5 books in the library.\n");
                }
            }
            else if (input.ToLower() == "remove" || input == "2")
            {
                // 7. Conditional Actions: Only allow removing books if there are books in the library.
                if (books.Count != 0)
                    RemoveBook();
                else
                {
                    Console.WriteLine("There are no books in the library to remove.\n");
                }
            }
            else if (input.ToLower() == "display" || input == "3")
            {
                if (books.Count != 0)
                    DisplayBooks();
                else
                {
                    Console.WriteLine("There are no books to display in the library.\n");
                }
            }
            else if (input.ToLower() == "search" || input == "4")
            {
                if (books.Count != 0)
                    SearchForBook();
                else
                {
                    Console.WriteLine("There are no books to search in the library.\n");
                }
            }
            else if (input.ToLower() == "check in" || input == "5")
            {
                if (books.Count != 0 && !NoBooksBorrowed)
                    CheckInBook();
                else
                {
                    Console.WriteLine("There are no books to check in.\n");
                }
            }
            else if (input.ToLower() == "exit" || input == "6")
            {
                Console.WriteLine("Have a nice day!\n");
                continueRunning = false;
            }
            else
            {
                Console.WriteLine("Please input a valid command: Add, Remove, Display, or Exit\n");
            }
        }
    }

    // 2. Add a Book: Prompt the user to input a book. Check which book variable is empty and store the new book in that variable. If all book slots are full, inform the user that no more books can be added.
    public static void AddBook()
    {
        Console.WriteLine("Enter the title of the book you want to add:");
        string titleInput = Console.ReadLine();
        if (string.IsNullOrEmpty(titleInput))
        {
            Console.WriteLine("Please write a valid book title.");
            AddBook();
            return;
        }

        Console.WriteLine("Enter the author of the book:");
        string authorInput = Console.ReadLine();
        if (string.IsNullOrEmpty(authorInput))
        {
            Console.WriteLine("Please write a valid author name.");
            AddBook();
            return;
        }

        Console.WriteLine("Enter the genre of the book:\n0 - Fantasy\n1 - ScienceFiction\n2 - Horror\n3 - Romance\n4 - Mystery\n5 - Adventure");
        string genreInput = Console.ReadLine();
        if (string.IsNullOrEmpty(genreInput) || !int.TryParse(genreInput, out int genreValue) || !Enum.IsDefined(typeof(Genre), genreValue))
        {
            Console.WriteLine("Please enter a valid genre number (0-5).");
            AddBook();
            return;
        }
        Genre selectedGenre = (Genre)genreValue;

        int numCopies = 1; // Default to 1 copy

        Console.WriteLine("Does this book have more than one copy? (yes/no):");
        string multipleCopiesInput = Console.ReadLine();

        if (multipleCopiesInput?.ToLower() == "yes" || multipleCopiesInput?.ToLower() == "y")
        {
            Console.WriteLine("Enter the number of copies:");
            string copiesInput = Console.ReadLine();
            
            if (!int.TryParse(copiesInput, out numCopies) || numCopies < 1)
            {
                Console.WriteLine("Please enter a valid number of copies (must be 1 or greater).");
                AddBook();
                return;
            }
        }

        // Create the book with all the collected information
        Book newBook = new(titleInput, authorInput, selectedGenre, numCopies);
        
        if (books.Any(b => b.Equals(newBook)))
        {
            Console.WriteLine("That book already exists in the library!");
            return;
        }
        
        books.Add(newBook);

        Console.WriteLine($"{titleInput} has been added to the library \n");
    }

    // 3. Remove a Book: Ask the user to input the Title of the book they want to remove. Check if the Title exists in the collection and, if found, clear the corresponding variable.
    public static void RemoveBook()
    {
        Console.WriteLine("Enter the Title of the book you want to remove:");
        string input = Console.ReadLine();

        while (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Please write a valid book Title");
            input = Console.ReadLine();
        }

        while (!books.Any(b => string.Equals(b.Title, input, StringComparison.CurrentCultureIgnoreCase)))
        {
            Console.WriteLine("This book does not exist in the collection. You may remove one of the following:");
            DisplayBooksOnly();
            Console.WriteLine("");
            input = Console.ReadLine();
        }

        books.RemoveAll(b => string.Equals(b.Title, input, StringComparison.CurrentCultureIgnoreCase));
        Console.WriteLine($"{input} has been removed from the library.\n");
    }

    // 4. Display the List of Books: Print out the list of books currently in the library, showing only the non-empty book slots.
    public static void DisplayBooks()
    {
        foreach (Book book in books)
        {
            Console.WriteLine($"{book.Title}");
        }

        Console.WriteLine("");
    }

    // Helper method for displaying books without extra formatting
    public static void DisplayBooksOnly()
    {
        foreach (Book book in books)
        {
            Console.WriteLine($"{book.Title}");
        }
    }

    // Part 3: Add a Search Feature
    public static void SearchForBook()
    {
    // Prompt the user to choose search type
    Console.WriteLine("How would you like to search for a book?");
    Console.WriteLine("1 - Title");
    Console.WriteLine("2 - Author");
    Console.WriteLine("3 - Genre");
    Console.Write("Enter your choice:");
    
    string searchTypeInput = Console.ReadLine();
    
    while (string.IsNullOrEmpty(searchTypeInput) || !int.TryParse(searchTypeInput, out int searchType) || searchType < 1 || searchType > 3)
    {
        Console.WriteLine("Please enter a valid choice (1-3):");
        searchTypeInput = Console.ReadLine();
    }
    
    int searchChoice = int.Parse(searchTypeInput);
    List<Book> matchingBooks = new List<Book>();
    
    switch (searchChoice)
    {
        case 1: // Search by title
            Console.WriteLine("Enter the title of the book you're looking for:");
            string titleInput = Console.ReadLine();
            
            while (string.IsNullOrEmpty(titleInput))
            {
                Console.WriteLine("Please write a valid book title:");
                titleInput = Console.ReadLine();
            }
            
            matchingBooks = books.Where(b => b.Title.Contains(titleInput, StringComparison.CurrentCultureIgnoreCase)).ToList();
            break;
            
        case 2: // Search by author
            Console.WriteLine("Enter the author name you're looking for:");
            string authorInput = Console.ReadLine();
            
            while (string.IsNullOrEmpty(authorInput))
            {
                Console.WriteLine("Please write a valid author name:");
                authorInput = Console.ReadLine();
            }
            
            matchingBooks = books.Where(b => b.Author.Contains(authorInput, StringComparison.CurrentCultureIgnoreCase)).ToList();
            break;
            
        case 3: // Search by genre
            Console.WriteLine("Select a genre:");
            Console.WriteLine("0 - Fantasy");
            Console.WriteLine("1 - ScienceFiction");
            Console.WriteLine("2 - Horror");
            Console.WriteLine("3 - Romance");
            Console.WriteLine("4 - Mystery");
            Console.WriteLine("5 - Adventure");
            
            string genreInput = Console.ReadLine();
            
            while (string.IsNullOrEmpty(genreInput) || !int.TryParse(genreInput, out int genreValue) || !Enum.IsDefined(typeof(Genre), genreValue))
            {
                Console.WriteLine("Please enter a valid genre number (0-5):");
                genreInput = Console.ReadLine();
            }
            
            Genre selectedGenre = (Genre)int.Parse(genreInput);
            matchingBooks = books.Where(b => b.Genre == selectedGenre).ToList();
            break;
    }
    
    // Handle search results
    if (matchingBooks.Count == 0)
    {
        Console.WriteLine("No books found matching your search criteria.");
        return;
    }
    
    if (matchingBooks.Count == 1)
    {
        // Single book found - proceed with the original logic
        Book searchedBook = matchingBooks[0];
        Console.WriteLine($"Found: {searchedBook.Title} by {searchedBook.Author} ({searchedBook.Genre})");
        
        if (searchedBook.CheckedOut)
        {
            Console.WriteLine("This book is currently checked out.\n");
            return;
        }
        
        if (BorrowLimitHit)
        {
            Console.WriteLine($"You have reached the maximum borrow limit of {MaxBorrowLimit} books.");
            return;
        }
        
        BorrowBook(searchedBook);
    }
    else
    {
        // Multiple books found - let user choose
        Console.WriteLine($"Found {matchingBooks.Count} matching books:");
        for (int i = 0; i < matchingBooks.Count; i++)
        {
            var book = matchingBooks[i];
            string status = book.CheckedOut ? " (Checked Out)" : " (Available)";
            Console.WriteLine($"{i + 1}. {book.Title} by {book.Author} ({book.Genre}){status}");
        }
        
        Console.WriteLine("Enter the number of the book you want to select (or 0 to cancel):");
        string selectionInput = Console.ReadLine();
        
        if (int.TryParse(selectionInput, out int selection) && selection >= 1 && selection <= matchingBooks.Count)
        {
            Book selectedBook = matchingBooks[selection - 1];
            
            if (selectedBook.CheckedOut)
            {
                Console.WriteLine("This book is currently checked out.\n");
                return;
            }
            
            if (BorrowLimitHit)
            {
                Console.WriteLine($"You have reached the maximum borrow limit of {MaxBorrowLimit} books.");
                return;
            }
            
            BorrowBook(selectedBook);
        }
        else if (selection == 0)
        {
            Console.WriteLine("Search cancelled.\n");
        }
        else
        {
            Console.WriteLine("Invalid selection.\n");
        }
    }
}

    public static void BorrowBook(Book bookToBorrow)
    {
        Console.WriteLine($"{bookToBorrow.Title} is available for borrowing.\nWould you like to borrow it? (Yes or No)");
        string newInput = Console.ReadLine();

        if (newInput?.ToLower() == "yes" || newInput?.ToLower() == "y")
        {
            Book.checkOut(bookToBorrow);
            Console.WriteLine($"You have borrowed: {bookToBorrow.Title}\n");
        }
        else if (newInput?.ToLower() == "no" || newInput?.ToLower() == "n")
        {
            // Do nothing, return to main menu
        }
        else
        {
            Console.WriteLine("Please write a valid response.");
            BorrowBook(bookToBorrow);
        }
    }

    // Part 3: Check In a borrowed book
    public static void CheckInBook()
    {
        List<Book> borrowedBooks = new List<Book>();
        // Check if the book is checked out. If it is remove the checked-out flag to check the book in. If it isn't, inform the user.
        Console.WriteLine("Choose a book to check in:");

        foreach (Book book in books)
        {
            if (book.CheckedOut)
            {
                Console.WriteLine($"{book.Title}");
                borrowedBooks.Add(book);
            }
        }

        string input = Console.ReadLine();
        Book selectedBook = borrowedBooks.FirstOrDefault(b => string.Equals(b.Title, input, StringComparison.CurrentCultureIgnoreCase));
        if (selectedBook != null)
        {
            Book.checkIn(selectedBook);
            Console.WriteLine($"{selectedBook.Title} has been checked in");
        }
        else
        {
            Console.WriteLine("Please write a valid book Title");
            CheckInBook();
        }
    }
}