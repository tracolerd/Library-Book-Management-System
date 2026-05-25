/**
 * Abstract base class representing a Book in the library.
 * Demonstrates encapsulation with private fields and public getters/setters.
 */
public abstract class Book {
    private static int idCounter = 1000; // Auto-incrementing ID starting from 1000

    private int bookId;
    private String title;
    private String author;
    private boolean isBorrowed;

    public Book(String title, String author) {
        this.bookId = ++idCounter;
        this.title = title;
        this.author = author;
        this.isBorrowed = false;
    }

    // Encapsulation: Getter methods
    public int getBookId() {
        return bookId;
    }

    public String getTitle() {
        return title;
    }

    public String getAuthor() {
        return author;
    }

    public boolean isBorrowed() {
        return isBorrowed;
    }

    // Setter for borrow status
    public void setBorrowed(boolean borrowed) {
        isBorrowed = borrowed;
    }

    /**
     * Polymorphic method to get book type.
     * Subclasses must override this method.
     */
    public abstract String getBookType();

    /**
     * Polymorphic method to display book details.
     * Can be overridden by subclasses for specialized display.
     */
    public void displayDetails() {
        System.out.println("+------+------------------------------+------------------------------+---------------+-------------+");
        System.out.printf("| %-4d | %-28s | %-28s | %-13s | %-11s |%n",
                bookId, title, author, getBookType(), isBorrowed ? "Borrowed" : "Available");
    }

    @Override
    public String toString() {
        return "Book ID: " + bookId + ", Title: " + title + ", Author: " + author
                + ", Type: " + getBookType() + ", Status: " + (isBorrowed ? "Borrowed" : "Available");
    }
}
