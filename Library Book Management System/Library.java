import java.util.ArrayList;

/**
 * Library class manages the collection of books.
 * Handles adding, borrowing, returning, and displaying books.
 * Uses ArrayList<Book> for data storage (polymorphic collection).
 */
public class Library {
    private ArrayList<Book> books;

    public Library() {
        books = new ArrayList<>();
    }

    /**
     * Add a new book to the library collection.
     */
    public void addBook(Book book) {
        books.add(book);
        System.out.println("\n  [SUCCESS] Book added successfully!");
        System.out.println("  " + book.toString());
    }

    /**
     * Borrow a book by its ID.
     * Throws BookNotAvailableException if book is not found or already borrowed.
     */
    public void borrowBook(int bookId) throws BookNotAvailableException {
        Book book = findBookById(bookId);
        if (book == null) {
            throw new BookNotAvailableException("Book with ID " + bookId + " not found in the library.");
        }
        if (book.isBorrowed()) {
            throw new BookNotAvailableException("Book '" + book.getTitle() + "' is already borrowed.");
        }
        book.setBorrowed(true);
        System.out.println("\n  [SUCCESS] You have successfully borrowed: " + book.getTitle());
    }

    /**
     * Return a borrowed book by its ID.
     * Throws BookNotAvailableException if book is not found or not borrowed.
     */
    public void returnBook(int bookId) throws BookNotAvailableException {
        Book book = findBookById(bookId);
        if (book == null) {
            throw new BookNotAvailableException("Book with ID " + bookId + " not found in the library.");
        }
        if (!book.isBorrowed()) {
            throw new BookNotAvailableException("Book '" + book.getTitle() + "' is not currently borrowed.");
        }
        book.setBorrowed(false);
        System.out.println("\n  [SUCCESS] You have successfully returned: " + book.getTitle());
    }

    /**
     * Display all books in the library.
     * Uses polymorphic displayDetails() method.
     */
    public void displayAllBooks() {
        if (books.isEmpty()) {
            System.out.println("\n  [INFO] No books in the library.");
            return;
        }
        printTableHeader("ALL BOOKS");
        for (Book book : books) {
            book.displayDetails(); // Polymorphic method call
        }
        printTableFooter();
        System.out.println("  Total books: " + books.size());
    }

    /**
     * Display only available (not borrowed) books.
     */
    public void displayAvailableBooks() {
        ArrayList<Book> available = new ArrayList<>();
        for (Book book : books) {
            if (!book.isBorrowed()) {
                available.add(book);
            }
        }
        if (available.isEmpty()) {
            System.out.println("\n  [INFO] No available books at the moment.");
            return;
        }
        printTableHeader("AVAILABLE BOOKS");
        for (Book book : available) {
            book.displayDetails(); // Polymorphic method call
        }
        printTableFooter();
        System.out.println("  Available books: " + available.size());
    }

    /**
     * Display only borrowed books.
     */
    public void displayBorrowedBooks() {
        ArrayList<Book> borrowed = new ArrayList<>();
        for (Book book : books) {
            if (book.isBorrowed()) {
                borrowed.add(book);
            }
        }
        if (borrowed.isEmpty()) {
            System.out.println("\n  [INFO] No books are currently borrowed.");
            return;
        }
        printTableHeader("BORROWED BOOKS");
        for (Book book : borrowed) {
            book.displayDetails(); // Polymorphic method call
        }
        printTableFooter();
        System.out.println("  Borrowed books: " + borrowed.size());
    }

    /**
     * Helper method to find a book by its ID.
     */
    private Book findBookById(int bookId) {
        for (Book book : books) {
            if (book.getBookId() == bookId) {
                return book;
            }
        }
        return null;
    }

    /**
     * Print formatted table header.
     */
    private void printTableHeader(String title) {
        System.out.println("\n  ======== " + title + " ========");
    }

    /**
     * Print formatted table footer.
     */
    private void printTableFooter() {
        System.out.println("+------+------------------------------+------------------------------+---------------+-------------+--------------------------+");
    }
}
