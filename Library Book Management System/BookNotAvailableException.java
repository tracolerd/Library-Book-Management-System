/**
 * Custom Exception class for handling book availability errors.
 * Thrown when a user tries to borrow an already borrowed book
 * or perform an invalid operation on a book.
 */
public class BookNotAvailableException extends Exception {

    public BookNotAvailableException(String message) {
        super(message);
    }

    public BookNotAvailableException(String message, Throwable cause) {
        super(message, cause);
    }
}
