using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace Docx;

public static class Program
{
    private static class Colors
    {
        public const string Primary = "1a5276";
        public const string Secondary = "2d6a6a";
        public const string Accent = "4a90a4";
        public const string Dark = "1c2833";
        public const string Mid = "566573";
        public const string Light = "909497";
        public const string Border = "d5d8dc";
        public const string TableHeader = "eaf2f8";
    }

    private const int A4W = 11906;
    private const int A4H = 16838;
    private const long A4WE = 7560000L;
    private const long A4HE = 10692000L;

    public static void Main(string[] args)
    {
        string outputPath = args.Length > 0 ? args[0] : "/mnt/agents/output/library_system/Library_Project_Report.docx";
        string bgDir = "/mnt/agents/output/library_system";
        Generate(outputPath, bgDir);
    }

    public static void Generate(string outputPath, string bgDir)
    {
        using var doc = WordprocessingDocument.Create(outputPath, WordprocessingDocumentType.Document);
        var mainPart = doc.AddMainDocumentPart();
        mainPart.Document = new Document(new Body());
        var body = mainPart.Document.Body!;

        AddStyles(mainPart);
        AddNumbering(mainPart);

        var coverBgId = AddImage(mainPart, Path.Combine(bgDir, "cover_bg.png"));
        var backBgId = AddImage(mainPart, Path.Combine(bgDir, "backcover_bg.png"));

        uint prId = 1;
        AddCoverSection(body, coverBgId, ref prId);
        AddTocSection(body);
        AddContentSection(doc, body, mainPart, bgDir, ref prId);
        AddBackcoverSection(body, backBgId, ref prId);

        SetUpdateFieldsOnOpen(mainPart);
        doc.Save();
    }

    private static void AddStyles(MainDocumentPart mainPart)
    {
        var sp = mainPart.AddNewPart<StyleDefinitionsPart>();
        sp.Styles = new Styles();

        sp.Styles.Append(new Style(
            new StyleName { Val = "Normal" },
            new StyleParagraphProperties(
                new SpacingBetweenLines { After = "200", Line = "312", LineRule = LineSpacingRuleValues.Auto }),
            new StyleRunProperties(
                new RunFonts { Ascii = "Calibri", HighAnsi = "Calibri", EastAsia = "Microsoft YaHei" },
                new FontSize { Val = "22" },
                new Color { Val = Colors.Dark })
        ) { Type = StyleValues.Paragraph, StyleId = "Normal", Default = true });

        sp.Styles.Append(CreateHeadingStyle("Heading1", "heading 1", 0, "36", Colors.Primary, "600", "240"));
        sp.Styles.Append(CreateHeadingStyle("Heading2", "heading 2", 1, "28", Colors.Dark, "400", "160"));
        sp.Styles.Append(CreateHeadingStyle("Heading3", "heading 3", 2, "24", Colors.Mid, "280", "120"));

        sp.Styles.Append(new Style(
            new StyleName { Val = "Caption" }, new BasedOn { Val = "Normal" },
            new StyleParagraphProperties(
                new Justification { Val = JustificationValues.Center },
                new SpacingBetweenLines { Before = "60", After = "320" }),
            new StyleRunProperties(new Color { Val = Colors.Light }, new FontSize { Val = "20" })
        ) { Type = StyleValues.Paragraph, StyleId = "Caption" });

        sp.Styles.Append(CreateTocStyle("TOC1", "toc 1", true, "0", "200"));
        sp.Styles.Append(CreateTocStyle("TOC2", "toc 2", false, "360", "60"));
    }

    private static Style CreateHeadingStyle(string id, string name, int level,
        string fontSize, string color, string spaceBefore, string spaceAfter)
    {
        return new Style(
            new StyleName { Val = name }, new BasedOn { Val = "Normal" },
            new StyleParagraphProperties(
                new KeepNext(), new KeepLines(),
                new SpacingBetweenLines { Before = spaceBefore, After = spaceAfter },
                new OutlineLevel { Val = level }),
            new StyleRunProperties(
                new Bold(), new FontSize { Val = fontSize },
                new RunFonts { Ascii = "Calibri", HighAnsi = "Calibri", EastAsia = "Microsoft YaHei" },
                new Color { Val = color })
        ) { Type = StyleValues.Paragraph, StyleId = id };
    }

    private static Style CreateTocStyle(string id, string name, bool bold, string indent, string before)
    {
        var rpr = new StyleRunProperties(new Color { Val = bold ? Colors.Dark : Colors.Mid });
        if (bold) rpr.Append(new Bold());
        return new Style(
            new StyleName { Val = name }, new BasedOn { Val = "Normal" },
            new StyleParagraphProperties(
                new Tabs(new TabStop { Val = TabStopValues.Right, Leader = TabStopLeaderCharValues.Dot, Position = 9350 }),
                new SpacingBetweenLines { Before = before, After = "60" },
                new Indentation { Left = indent }),
            rpr
        ) { Type = StyleValues.Paragraph, StyleId = id };
    }

    private static void AddCoverSection(Body body, string coverBgId, ref uint prId)
    {
        body.Append(new Paragraph(new Run(CreateFloatingBackground(coverBgId, prId++, "CoverBg"))));
        body.Append(new Paragraph(new ParagraphProperties(new SpacingBetweenLines { Before = "5000" }), new Run()));

        body.Append(new Paragraph(
            new ParagraphProperties(
                new Justification { Val = JustificationValues.Center },
                new SpacingBetweenLines { After = "200" }),
            new Run(new RunProperties(
                    new FontSize { Val = "72" }, new Bold(),
                    new Color { Val = Colors.Primary },
                    new Spacing { Val = 30 }),
                new Text("Library Book"))));

        body.Append(new Paragraph(
            new ParagraphProperties(
                new Justification { Val = JustificationValues.Center },
                new SpacingBetweenLines { After = "400" }),
            new Run(new RunProperties(
                    new FontSize { Val = "72" }, new Bold(),
                    new Color { Val = Colors.Primary },
                    new Spacing { Val = 30 }),
                new Text("Management System"))));

        body.Append(new Paragraph(
            new ParagraphProperties(
                new Justification { Val = JustificationValues.Center },
                new SpacingBetweenLines { After = "600" }),
            new Run(new RunProperties(
                    new FontSize { Val = "28" },
                    new Color { Val = Colors.Secondary }),
                new Text("CSE110: Object-Oriented Programming"))));

        body.Append(new Paragraph(
            new ParagraphProperties(
                new Justification { Val = JustificationValues.Center },
                new SpacingBetweenLines { After = "200" }),
            new Run(new RunProperties(new FontSize { Val = "24" }, new Color { Val = Colors.Mid }),
                new Text("Mini Project Report - Spring 2026"))));

        body.Append(new Paragraph(
            new ParagraphProperties(new SpacingBetweenLines { Before = "2000" },
                new Justification { Val = JustificationValues.Center }),
            new Run(new RunProperties(new FontSize { Val = "22" }, new Color { Val = Colors.Mid }),
                new Text("A Console-Based Interactive Application in Java"))));

        body.Append(new Paragraph(new ParagraphProperties(new SectionProperties(
            new TitlePage(),
            new SectionType { Val = SectionMarkValues.NextPage },
            new PageSize { Width = (UInt32Value)(uint)A4W, Height = (UInt32Value)(uint)A4H },
            new PageMargin { Top = 0, Right = 0, Bottom = 0, Left = 0, Header = 0, Footer = 0 }))));
    }

    private static void AddTocSection(Body body)
    {
        body.Append(CreateHeading1("Table of Contents", "_Toc000"));

        body.Append(new Paragraph(
            new ParagraphProperties(new SpacingBetweenLines { After = "300" }),
            new Run(new RunProperties(new Color { Val = Colors.Light }, new FontSize { Val = "18" }),
                new Text("Right-click and select \"Update Field\" to refresh page numbers"))));

        body.Append(new Paragraph(
            new Run(new FieldChar { FieldCharType = FieldCharValues.Begin }),
            new Run(new FieldCode(" TOC \\o \"1-3\" \\h \\z \\u ") { Space = SpaceProcessingModeValues.Preserve }),
            new Run(new FieldChar { FieldCharType = FieldCharValues.Separate })));

        string[,] toc = {
            { "Introduction", "1", "3" }, { "System Design", "1", "4" },
            { "Class Diagram and Architecture", "2", "4" }, { "Class Descriptions", "2", "5" },
            { "Implementation", "1", "7" }, { "OOP Concepts Demonstrated", "2", "7" },
            { "Exception Handling", "2", "8" }, { "Testing", "1", "9" },
            { "Test Cases and Results", "2", "9" }, { "Conclusion", "1", "10" },
            { "References", "1", "11" },
        };
        for (int i = 0; i < toc.GetLength(0); i++)
            body.Append(new Paragraph(
                new ParagraphProperties(new ParagraphStyleId { Val = $"TOC{toc[i, 1]}" }),
                new Run(new Text(toc[i, 0])), new Run(new TabChar()), new Run(new Text(toc[i, 2]))));

        body.Append(new Paragraph(new Run(new FieldChar { FieldCharType = FieldCharValues.End })));

        body.Append(new Paragraph(new ParagraphProperties(new SectionProperties(
            new SectionType { Val = SectionMarkValues.NextPage },
            new PageSize { Width = (UInt32Value)(uint)A4W, Height = (UInt32Value)(uint)A4H },
            new PageMargin { Top = 1800, Right = 1440, Bottom = 1440, Left = 1440, Header = 720, Footer = 720 }))));
    }

    private static void AddContentSection(WordprocessingDocument doc, Body body,
        MainDocumentPart mainPart, string bgDir, ref uint prId)
    {
        var headerPart = mainPart.AddNewPart<HeaderPart>();
        var headerId = mainPart.GetIdOfPart(headerPart);
        headerPart.Header = new Header(new Paragraph(
            new ParagraphProperties(new Justification { Val = JustificationValues.Right }),
            new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Light }),
                new Text("CSE110 Mini Project Report"))));

        var footerPart = mainPart.AddNewPart<FooterPart>();
        var footerId = mainPart.GetIdOfPart(footerPart);
        var fp = new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }));
        fp.Append(new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Light }),
            new FieldChar { FieldCharType = FieldCharValues.Begin }));
        fp.Append(new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Light }),
            new FieldCode(" PAGE ") { Space = SpaceProcessingModeValues.Preserve }));
        fp.Append(new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Light }),
            new FieldChar { FieldCharType = FieldCharValues.Separate }));
        fp.Append(new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Light }),
            new Text("1")));
        fp.Append(new Run(new RunProperties(new FontSize { Val = "18" }, new Color { Val = Colors.Light }),
            new FieldChar { FieldCharType = FieldCharValues.End }));
        footerPart.Footer = new Footer(fp);

        // === 1. Introduction ===
        body.Append(CreateHeading1("1. Introduction", "_Toc001"));
        body.Append(CreateParagraph("This report presents the design and implementation of a Library Book Management System, developed as a mini project for the CSE110: Object-Oriented Programming course in Spring 2026. The system is a console-based interactive application written in Java that simulates the core operations of a small library."));
        body.Append(CreateParagraph("The primary objective of this project is to demonstrate a thorough understanding and practical application of fundamental object-oriented programming (OOP) concepts. These include classes and objects, encapsulation, inheritance, polymorphism, and exception handling. The program provides a menu-driven interface that allows users to add books, borrow and return books, and view the library's inventory in various formats."));
        body.Append(CreateHeading2("1.1 Project Objectives"));
        body.Append(CreateBulletItem("Design and implement Java classes using OOP principles"));
        body.Append(CreateBulletItem("Apply encapsulation through private fields and public methods"));
        body.Append(CreateBulletItem("Implement inheritance with specialized book types"));
        body.Append(CreateBulletItem("Demonstrate polymorphism using method overriding"));
        body.Append(CreateBulletItem("Handle runtime errors using custom exception handling"));
        body.Append(CreateBulletItem("Develop a user-friendly, menu-driven console application"));

        // === 2. System Design ===
        body.Append(CreateHeading1("2. System Design", "_Toc002"));
        body.Append(CreateHeading2("2.1 Class Diagram and Architecture"));
        body.Append(CreateParagraph("The system follows a hierarchical class structure centered around the Book base class. The architecture separates concerns into distinct classes, each responsible for a specific aspect of the system's functionality."));
        body.Append(CreateParagraph("The class hierarchy is organized as follows:"));
        body.Append(CreateCodeBlock(
            "Book (abstract class)\n" +
            "  |-- AcademicBook (extends Book)\n" +
            "  |-- Storybook (extends Book)\n\n" +
            "BookNotAvailableException (extends Exception)\n\n" +
            "Library (manages ArrayList<Book>)\n\n" +
            "LibraryManagementSystem (main class with menu)"
        ));

        body.Append(CreateHeading2("2.2 Class Descriptions"));
        body.Append(CreateClassTable());

        body.Append(CreateHeading3("Book.java (Abstract Base Class)"));
        body.Append(CreateParagraph("The Book class serves as the abstract base class for all book types. It encapsulates common properties including bookId (auto-incremented), title, author, and isBorrowed status. The class declares an abstract method getBookType() and a polymorphic displayDetails() method that subclasses override to provide type-specific information."));

        body.Append(CreateHeading3("AcademicBook.java"));
        body.Append(CreateParagraph("Inherits from Book and adds a subject field specific to academic materials. Overrides getBookType() to return \"Academic\" and displayDetails() to include the subject in the output."));

        body.Append(CreateHeading3("Storybook.java"));
        body.Append(CreateParagraph("Inherits from Book and adds a recommendedAge field for age-appropriate reading guidance. Overrides getBookType() to return \"Storybook\" and displayDetails() to show the recommended age."));

        body.Append(CreateHeading3("BookNotAvailableException.java"));
        body.Append(CreateParagraph("A custom checked exception class that extends Exception. It is thrown when attempting to borrow an already borrowed book or when referencing a non-existent book ID."));

        body.Append(CreateHeading3("Library.java"));
        body.Append(CreateParagraph("The core management class that maintains an ArrayList<Book> collection. It provides methods for adding books, borrowing books, returning books, and displaying books in various formats. All operations use polymorphic method calls to handle different book types uniformly."));

        body.Append(CreateHeading3("LibraryManagementSystem.java"));
        body.Append(CreateParagraph("The main class containing the entry point and menu-driven user interface. It handles user input, delegates operations to the Library class, and manages exception handling for invalid inputs and operations."));

        // === 3. Implementation ===
        body.Append(CreateHeading1("3. Implementation", "_Toc003"));
        body.Append(CreateHeading2("3.1 OOP Concepts Demonstrated"));

        body.Append(CreateHeading3("Encapsulation"));
        body.Append(CreateParagraph("All fields in the Book class are declared private, preventing direct external modification. Controlled access is provided through public getter and setter methods. For example, the isBorrowed status can only be modified through the setBorrowed() method, ensuring proper state management."));

        body.Append(CreateHeading3("Inheritance"));
        body.Append(CreateParagraph("Both AcademicBook and Storybook extend the Book base class, inheriting all its fields and methods. They add type-specific attributes (subject and recommendedAge respectively) while reusing the common functionality from the parent class. This eliminates code duplication and establishes a natural is-a relationship."));

        body.Append(CreateHeading3("Polymorphism"));
        body.Append(CreateParagraph("The system demonstrates polymorphism in two key ways. First, method overriding: each subclass overrides getBookType() and displayDetails() to provide type-specific behavior. Second, the Library class uses polymorphic references - books are stored as ArrayList<Book> but actual objects are AcademicBook or Storybook instances. When displayDetails() is called, Java dynamically dispatches to the appropriate subclass implementation."));

        body.Append(CreateHeading2("3.2 Exception Handling"));
        body.Append(CreateParagraph("The system implements comprehensive exception handling using both built-in and custom exceptions:"));
        body.Append(CreateBulletItem("InputMismatchException: Caught when users enter non-numeric values where numbers are expected"));
        body.Append(CreateBulletItem("BookNotAvailableException: Custom exception thrown when borrowing an already borrowed book or returning an available book"));
        body.Append(CreateBulletItem("Invalid menu choices are detected through conditional checks with appropriate error messages"));
        body.Append(CreateParagraph("All exceptions are handled gracefully, displaying user-friendly error messages and allowing the program to continue running without crashing."));

        // === 4. Testing ===
        body.Append(CreateHeading1("4. Testing", "_Toc004"));
        body.Append(CreateHeading2("4.1 Test Cases and Results"));
        body.Append(CreateTestTable());
        body.Append(CreateParagraph("All test cases were executed successfully. The program handles edge cases gracefully and provides clear feedback for all user interactions. The menu system continues to prompt the user until the exit option is selected."));

        // === 5. Conclusion ===
        body.Append(CreateHeading1("5. Conclusion", "_Toc005"));
        body.Append(CreateParagraph("This project successfully demonstrates the practical application of object-oriented programming principles in Java. The Library Book Management System implements all required functionality including adding, borrowing, returning, and displaying books through an intuitive menu-driven interface."));
        body.Append(CreateParagraph("Key achievements of this project include proper use of encapsulation to protect data integrity, inheritance to create specialized book types without code duplication, polymorphism to handle different book types uniformly, and robust exception handling to prevent runtime crashes."));
        body.Append(CreateParagraph("The modular class design makes the system extensible - new book types can be added by simply creating new subclasses of Book, and additional features like file-based persistence or a graphical user interface could be integrated with minimal changes to the existing codebase."));
        body.Append(CreateHeading2("5.1 Key Learnings"));
        body.Append(CreateBulletItem("OOP design patterns promote code reusability and maintainability"));
        body.Append(CreateBulletItem("Polymorphism enables flexible and extensible software architecture"));
        body.Append(CreateBulletItem("Custom exceptions improve error handling clarity"));
        body.Append(CreateBulletItem("Separation of concerns through distinct classes simplifies debugging and testing"));

        // === 6. References ===
        body.Append(CreateHeading1("6. References", "_Toc006"));
        body.Append(CreateNumberedRef("Liang, Y.D. (2020). Introduction to Java Programming, Comprehensive Version. 12th Edition. Pearson."));
        body.Append(CreateNumberedRef("Oracle Corporation. (2024). The Java Tutorials - Object-Oriented Programming Concepts. docs.oracle.com/javase/tutorial/java/concepts/"));
        body.Append(CreateNumberedRef("Bloch, J. (2018). Effective Java. 3rd Edition. Addison-Wesley Professional."));

        body.Append(new Paragraph(new ParagraphProperties(new SectionProperties(
            new HeaderReference { Type = HeaderFooterValues.Default, Id = headerId },
            new FooterReference { Type = HeaderFooterValues.Default, Id = footerId },
            new PageSize { Width = (UInt32Value)(uint)A4W, Height = (UInt32Value)(uint)A4H },
            new PageMargin { Top = 1800, Right = 1440, Bottom = 1440, Left = 1440, Header = 720, Footer = 720 }))));
    }

    private static void AddBackcoverSection(Body body, string backBgId, ref uint prId)
    {
        body.Append(new Paragraph(new Run(CreateFloatingBackground(backBgId, prId++, "BackBg"))));

        body.Append(new Paragraph(
            new ParagraphProperties(new SpacingBetweenLines { Before = "7000" },
                new Justification { Val = JustificationValues.Center }),
            new Run(new RunProperties(new FontSize { Val = "48" }, new Bold(),
                new Color { Val = Colors.Primary }),
                new Text("Thank You"))));

        body.Append(new Paragraph(
            new ParagraphProperties(new SpacingBetweenLines { Before = "400" },
                new Justification { Val = JustificationValues.Center }),
            new Run(new RunProperties(new FontSize { Val = "22" }, new Color { Val = Colors.Mid }),
                new Text("CSE110: Object-Oriented Programming"))));

        body.Append(new Paragraph(
            new ParagraphProperties(new SpacingBetweenLines { Before = "200" },
                new Justification { Val = JustificationValues.Center }),
            new Run(new RunProperties(new FontSize { Val = "20" }, new Color { Val = Colors.Light }),
                new Text("Spring 2026"))));

        body.Append(new SectionProperties(
            new PageSize { Width = (UInt32Value)(uint)A4W, Height = (UInt32Value)(uint)A4H },
            new PageMargin { Top = 0, Right = 0, Bottom = 0, Left = 0, Header = 0, Footer = 0 }));
    }

    // === Factory Helpers ===
    private static int _bookmarkId = 0;

    private static Paragraph CreateHeading1(string text, string bookmarkName)
    {
        int id = ++_bookmarkId;
        return new Paragraph(
            new ParagraphProperties(new ParagraphStyleId { Val = "Heading1" }),
            new BookmarkStart { Id = id.ToString(), Name = bookmarkName },
            new Run(new Text(text)),
            new BookmarkEnd { Id = id.ToString() });
    }

    private static Paragraph CreateHeading2(string text)
    {
        return new Paragraph(
            new ParagraphProperties(new ParagraphStyleId { Val = "Heading2" }),
            new Run(new Text(text)));
    }

    private static Paragraph CreateHeading3(string text)
    {
        return new Paragraph(
            new ParagraphProperties(new ParagraphStyleId { Val = "Heading3" }),
            new Run(new Text(text)));
    }

    private static Paragraph CreateParagraph(string text)
    {
        return new Paragraph(new Run(new Text(text)));
    }

    private static Paragraph CreateBulletItem(string text)
    {
        return new Paragraph(
            new ParagraphProperties(
                new NumberingProperties(new NumberingLevelReference { Val = 0 }, new NumberingId { Val = 1 })),
            new Run(new Text(text)));
    }

    private static Paragraph CreateNumberedRef(string text)
    {
        return new Paragraph(
            new ParagraphProperties(new Indentation { Left = "720", Hanging = "360" }),
            new Run(new Text(text)));
    }

    private static Paragraph CreateCodeBlock(string text)
    {
        return new Paragraph(
            new ParagraphProperties(
                new Shading { Val = ShadingPatternValues.Clear, Fill = "f4f6f7" },
                new SpacingBetweenLines { Before = "200", After = "200" },
                new Indentation { Left = "400" }),
            new Run(new RunProperties(
                new RunFonts { Ascii = "Consolas", HighAnsi = "Consolas" },
                new FontSize { Val = "20" },
                new Color { Val = Colors.Dark }),
                new Text(text)));
    }

    private static Table CreateClassTable()
    {
        var tbl = new Table(new TableProperties(
            new TableWidth { Width = "5000", Type = TableWidthUnitValues.Pct },
            new TableBorders(
                new TopBorder { Val = BorderValues.Single, Size = 12, Color = Colors.Primary },
                new BottomBorder { Val = BorderValues.Single, Size = 12, Color = Colors.Primary },
                new InsideHorizontalBorder { Val = BorderValues.Single, Size = 4, Color = Colors.Border })),
            new TableGrid(
                new GridColumn { Width = "2200" },
                new GridColumn { Width = "1400" },
                new GridColumn { Width = "5400" }));

        var cw1 = new[] { "2200", "1400", "5400" };
        tbl.Append(CreateTableRow(true, cw1, "Class Name", "Type", "Description"));
        tbl.Append(CreateTableRow(false, cw1, "Book", "Abstract", "Base class with common book properties"));
        tbl.Append(CreateTableRow(false, cw1, "AcademicBook", "Subclass", "Academic textbooks with subject field"));
        tbl.Append(CreateTableRow(false, cw1, "Storybook", "Subclass", "Fiction books with age recommendation"));
        tbl.Append(CreateTableRow(false, cw1, "BookNotAvailableException", "Exception", "Custom exception for invalid operations"));
        tbl.Append(CreateTableRow(false, cw1, "Library", "Manager", "Manages book collection and operations"));
        tbl.Append(CreateTableRow(false, cw1, "LibraryManagementSystem", "Main", "Entry point with menu interface"));

        return tbl;
    }

    private static Table CreateTestTable()
    {
        var tbl = new Table(new TableProperties(
            new TableWidth { Width = "5000", Type = TableWidthUnitValues.Pct },
            new TableBorders(
                new TopBorder { Val = BorderValues.Single, Size = 12, Color = Colors.Primary },
                new BottomBorder { Val = BorderValues.Single, Size = 12, Color = Colors.Primary },
                new InsideHorizontalBorder { Val = BorderValues.Single, Size = 4, Color = Colors.Border })),
            new TableGrid(
                new GridColumn { Width = "800" },
                new GridColumn { Width = "2400" },
                new GridColumn { Width = "1800" },
                new GridColumn { Width = "1200" },
                new GridColumn { Width = "2800" }));

        var cw2 = new[] { "800", "2400", "1800", "1200", "2800" };
        tbl.Append(CreateTableRow(true, cw2, "ID", "Test Case", "Input", "Expected", "Result"));
        tbl.Append(CreateTableRow(false, cw2, "T1", "Add Academic Book", "Title, Author, Subject", "Book Added", "Pass"));
        tbl.Append(CreateTableRow(false, cw2, "T2", "Add Storybook", "Title, Author, Age", "Book Added", "Pass"));
        tbl.Append(CreateTableRow(false, cw2, "T3", "Borrow Available Book", "Valid Book ID", "Success Message", "Pass"));
        tbl.Append(CreateTableRow(false, cw2, "T4", "Borrow Already Borrowed", "Borrowed Book ID", "Exception Thrown", "Pass"));
        tbl.Append(CreateTableRow(false, cw2, "T5", "Return Borrowed Book", "Valid Book ID", "Success Message", "Pass"));
        tbl.Append(CreateTableRow(false, cw2, "T6", "Invalid Menu Input", "Non-numeric", "Error Message", "Pass"));
        tbl.Append(CreateTableRow(false, cw2, "T7", "Invalid Book ID", "Non-existent ID", "Exception Thrown", "Pass"));
        tbl.Append(CreateTableRow(false, cw2, "T8", "Display All Books", "Menu Option 4", "List Displayed", "Pass"));

        return tbl;
    }

    private static TableRow CreateTableRow(bool hdr, string[] widths, params string[] cells)
    {
        var row = new TableRow();
        if (hdr) row.Append(new TableRowProperties(new TableHeader()));
        for (int i = 0; i < cells.Length; i++)
        {
            var tcp = new TableCellProperties(new TableCellWidth { Width = widths[i], Type = TableWidthUnitValues.Dxa });
            if (hdr) tcp.Append(new Shading { Val = ShadingPatternValues.Clear, Fill = Colors.TableHeader });
            var rpr = new RunProperties(new FontSize { Val = "20" }, new Color { Val = hdr ? Colors.Dark : Colors.Mid });
            if (hdr) rpr.Append(new Bold());
            row.Append(new TableCell(tcp, new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                    new SpacingBetweenLines { Before = "40", After = "40" }),
                new Run(rpr, new Text(cells[i])))));
        }
        return row;
    }

    private static string AddImage(MainDocumentPart mp, string path)
    {
        var ip = mp.AddImagePart(ImagePartType.Png);
        using var fs = new FileStream(path, FileMode.Open);
        ip.FeedData(fs); return mp.GetIdOfPart(ip);
    }

    private static Drawing CreateFloatingBackground(string imgId, uint prId, string name)
    {
        return new Drawing(new DW.Anchor(
            new DW.SimplePosition { X = 0, Y = 0 },
            new DW.HorizontalPosition(new DW.PositionOffset("0")) { RelativeFrom = DW.HorizontalRelativePositionValues.Page },
            new DW.VerticalPosition(new DW.PositionOffset("0")) { RelativeFrom = DW.VerticalRelativePositionValues.Page },
            new DW.Extent { Cx = A4WE, Cy = A4HE },
            new DW.EffectExtent { LeftEdge = 0, TopEdge = 0, RightEdge = 0, BottomEdge = 0 },
            new DW.WrapNone(),
            new DW.DocProperties { Id = prId, Name = name },
            new DW.NonVisualGraphicFrameDrawingProperties(new A.GraphicFrameLocks { NoChangeAspect = true }),
            new A.Graphic(new A.GraphicData(
                new PIC.Picture(
                    new PIC.NonVisualPictureProperties(
                        new PIC.NonVisualDrawingProperties { Id = 0, Name = $"{name}.png" },
                        new PIC.NonVisualPictureDrawingProperties()),
                    new PIC.BlipFill(new A.Blip { Embed = imgId }, new A.Stretch(new A.FillRectangle())),
                    new PIC.ShapeProperties(
                        new A.Transform2D(new A.Offset { X = 0, Y = 0 }, new A.Extents { Cx = A4WE, Cy = A4HE }),
                        new A.PresetGeometry { Preset = A.ShapeTypeValues.Rectangle })))
            { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" }))
        { DistanceFromTop = 0, DistanceFromBottom = 0, DistanceFromLeft = 0, DistanceFromRight = 0,
          SimplePos = false, RelativeHeight = 251658240, BehindDoc = true,
          Locked = false, LayoutInCell = true, AllowOverlap = true });
    }

    private static void SetUpdateFieldsOnOpen(MainDocumentPart mp)
    {
        var sp = mp.DocumentSettingsPart ?? mp.AddNewPart<DocumentSettingsPart>();
        sp.Settings = new Settings(new UpdateFieldsOnOpen { Val = true }, new DisplayBackgroundShape());
    }

    private static void AddNumbering(MainDocumentPart mp)
    {
        var np = mp.AddNewPart<NumberingDefinitionsPart>();
        np.Numbering = new Numbering(
            new AbstractNum(new Level(
                new NumberingFormat { Val = NumberFormatValues.Bullet },
                new LevelText { Val = "\u2022" },
                new LevelJustification { Val = LevelJustificationValues.Left },
                new ParagraphProperties(new Indentation { Left = "720", Hanging = "360" })
            ) { LevelIndex = 0 }) { AbstractNumberId = 1 },
            new NumberingInstance(new AbstractNumId { Val = 1 }) { NumberID = 1 });
    }
}
