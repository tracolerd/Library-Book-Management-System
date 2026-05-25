/**
 * Storybook class inherits from Book.
 * Represents fiction and story books for leisure reading.
 * Demonstrates inheritance and polymorphism (method overriding).
 */
public class Storybook extends Book {
    private int recommendedAge;

    public Storybook(String title, String author, int recommendedAge) {
        super(title, author);
        this.recommendedAge = recommendedAge;
    }

    public int getRecommendedAge() {
        return recommendedAge;
    }

    /**
     * Polymorphism: Overriding getBookType() from Book class.
     */
    @Override
    public String getBookType() {
        return "Storybook";
    }

    /**
     * Polymorphism: Overriding displayDetails() to include age recommendation.
     */
    @Override
    public void displayDetails() {
        System.out.println("+------+------------------------------+------------------------------+---------------+-------------+--------------------------+");
        System.out.printf("| %-4d | %-28s | %-28s | %-13s | %-11s | Age: %-19d |%n",
                getBookId(), getTitle(), getAuthor(), getBookType(),
                isBorrowed() ? "Borrowed" : "Available", recommendedAge);
    }
}
