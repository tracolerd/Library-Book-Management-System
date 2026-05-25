/**
 * AcademicBook class inherits from Book.
 * Represents academic textbooks and reference materials.
 * Demonstrates inheritance and polymorphism (method overriding).
 */
public class AcademicBook extends Book {
    private String subject;

    public AcademicBook(String title, String author, String subject) {
        super(title, author);
        this.subject = subject;
    }

    public String getSubject() {
        return subject;
    }

    /**
     * Polymorphism: Overriding getBookType() from Book class.
     */
    @Override
    public String getBookType() {
        return "Academic";
    }

    /**
     * Polymorphism: Overriding displayDetails() to include subject information.
     */
    @Override
    public void displayDetails() {
        System.out.println("+------+------------------------------+------------------------------+---------------+-------------+--------------------------+");
        System.out.printf("| %-4d | %-28s | %-28s | %-13s | %-11s | Subject: %-15s |%n",
                getBookId(), getTitle(), getAuthor(), getBookType(),
                isBorrowed() ? "Borrowed" : "Available", subject);
    }
}
