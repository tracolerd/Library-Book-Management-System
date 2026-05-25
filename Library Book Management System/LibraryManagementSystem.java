import java.util.InputMismatchException;
import java.util.Scanner;

/**
 * Main class for the Library Book Management System.
 * Provides a menu-driven console interface for user interaction.
 * Demonstrates exception handling and OOP concepts.
 */
public class LibraryManagementSystem {

    public static void main(String[] args) {
        Library library = new Library();
        Scanner scanner = new Scanner(System.in);
        boolean running = true;

        displayWelcomeMessage();

        // Pre-populate with sample data for demonstration
        addSampleBooks(library);

        while (running) {
            displayMenu();
            try {
                System.out.print("\n  Enter your choice (1-7): ");
                int choice = scanner.nextInt();
                scanner.nextLine(); // Consume newline

                switch (choice) {
                    case 1:
                        addNewBook(library, scanner);
                        break;
                    case 2:
                        borrowBook(library, scanner);
                        break;
                    case 3:
                        returnBook(library, scanner);
                        break;
                    case 4:
                        library.displayAllBooks();
                        break;
                    case 5:
                        library.displayAvailableBooks();
                        break;
                    case 6:
                        library.displayBorrowedBooks();
                        break;
                    case 7:
                        running = false;
                        System.out.println("\n  Thank you for using Library Management System. Goodbye!");
                        break;
                    default:
                        System.out.println("\n  [ERROR] Invalid choice. Please enter a number between 1 and 7.");
                }
            } catch (InputMismatchException e) {
                System.out.println("\n  [ERROR] Invalid input. Please enter a valid number.");
                scanner.nextLine(); // Clear the invalid input
            } catch (BookNotAvailableException e) {
                System.out.println("\n  [ERROR] " + e.getMessage());
            } catch (Exception e) {
                System.out.println("\n  [ERROR] An unexpected error occurred: " + e.getMessage());
            }

            if (running) {
                System.out.print("\n  Press Enter to continue...");
                scanner.nextLine();
            }
        }

        scanner.close();
    }

    /**
     * Display welcome message at program startup.
     */
    private static void displayWelcomeMessage() {
        System.out.println("\n");
        System.out.println("  +============================================================+");
        System.out.println("  |                                                            |");
        System.out.println("  |         LIBRARY BOOK MANAGEMENT SYSTEM                     |");
        System.out.println("  |         CSE110 - Object Oriented Programming               |");
        System.out.println("  |                                                            |");
        System.out.println("  +============================================================+");
    }

    /**
     * Display the main menu options.
     */
    private static void displayMenu() {
        System.out.println("\n  +---------------------- MAIN MENU ---------------------------+");
        System.out.println("  |  [1] Add a New Book                                        |");
        System.out.println("  |  [2] Borrow a Book                                         |");
        System.out.println("  |  [3] Return a Book                                         |");
        System.out.println("  |  [4] Display All Books                                     |");
        System.out.println("  |  [5] Display Available Books                               |");
        System.out.println("  |  [6] Display Borrowed Books                                |");
        System.out.println("  |  [7] Exit                                                  |");
        System.out.println("  +------------------------------------------------------------+");
    }

    /**
     * Handle adding a new book with user input.
     */
    private static void addNewBook(Library library, Scanner scanner) {
        System.out.println("\n  +------------------- ADD NEW BOOK ---------------------------+");

        System.out.print("  Enter book title: ");
        String title = scanner.nextLine().trim();

        System.out.print("  Enter author name: ");
        String author = scanner.nextLine().trim();

        System.out.println("  Select book type:");
        System.out.println("    [1] Academic Book");
        System.out.println("    [2] Storybook");
        System.out.print("  Enter choice (1 or 2): ");

        try {
            int typeChoice = scanner.nextInt();
            scanner.nextLine(); // Consume newline

            Book newBook = null;

            if (typeChoice == 1) {
                System.out.print("  Enter subject (e.g., Mathematics, Physics): ");
                String subject = scanner.nextLine().trim();
                newBook = new AcademicBook(title, author, subject);
            } else if (typeChoice == 2) {
                System.out.print("  Enter recommended age: ");
                int age = scanner.nextInt();
                scanner.nextLine(); // Consume newline
                newBook = new Storybook(title, author, age);
            } else {
                System.out.println("\n  [ERROR] Invalid book type selection.");
                return;
            }

            library.addBook(newBook);

        } catch (InputMismatchException e) {
            System.out.println("\n  [ERROR] Invalid input. Please enter a valid number.");
            scanner.nextLine(); // Clear invalid input
        }
    }

    /**
     * Handle borrowing a book with user input.
     */
    private static void borrowBook(Library library, Scanner scanner) throws BookNotAvailableException {
        System.out.println("\n  +------------------- BORROW BOOK ---------------------------+");
        library.displayAvailableBooks();

        System.out.print("\n  Enter Book ID to borrow (or 0 to cancel): ");
        try {
            int bookId = scanner.nextInt();
            scanner.nextLine(); // Consume newline
            if (bookId != 0) {
                library.borrowBook(bookId);
            }
        } catch (InputMismatchException e) {
            System.out.println("\n  [ERROR] Invalid input. Please enter a valid Book ID.");
            scanner.nextLine(); // Clear invalid input
        }
    }

    /**
     * Handle returning a book with user input.
     */
    private static void returnBook(Library library, Scanner scanner) throws BookNotAvailableException {
        System.out.println("\n  +------------------- RETURN BOOK ---------------------------+");
        library.displayBorrowedBooks();

        System.out.print("\n  Enter Book ID to return (or 0 to cancel): ");
        try {
            int bookId = scanner.nextInt();
            scanner.nextLine(); // Consume newline
            if (bookId != 0) {
                library.returnBook(bookId);
            }
        } catch (InputMismatchException e) {
            System.out.println("\n  [ERROR] Invalid input. Please enter a valid Book ID.");
            scanner.nextLine(); // Clear invalid input
        }
    }

    /**
     * Add sample books to the library for demonstration purposes.
     */
    private static void addSampleBooks(Library library) {
        library.addBook(new AcademicBook("Introduction to Java Programming", "Daniel Liang", "Computer Science"));
        library.addBook(new AcademicBook("Data Structures and Algorithms", "Thomas Cormen", "Computer Science"));
        library.addBook(new Storybook("The Great Adventure", "John Smith", 12));
        library.addBook(new Storybook("Mystery of the Lost Key", "Emma Wilson", 10));
        library.addBook(new AcademicBook("Physics for Scientists", "Raymond Serway", "Physics"));
    }
}
