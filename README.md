# 📚 Library Book Management System

A **console-based Library Book Management System built with Java** to demonstrate core **Object-Oriented Programming (OOP)** concepts through a practical library simulation.

The application allows users to add books, borrow and return books, and view books based on their current availability. It also demonstrates **abstraction, encapsulation, inheritance, polymorphism, collections, and exception handling**.

---

## ✨ Features

* ➕ Add new books to the library
* 📖 Support multiple book types:

  * Academic Books
  * Storybooks
* 📥 Borrow available books
* 📤 Return borrowed books
* 📋 Display all books
* ✅ Display only available books
* 🔄 Display currently borrowed books
* 🆔 Automatically generate unique book IDs
* ⚠️ Handle invalid user input
* 🚫 Handle unavailable or invalid book operations using custom exceptions
* 🖥️ Simple interactive command-line interface

---

## 🧠 OOP Concepts Demonstrated

This project was designed primarily as an implementation of fundamental Java OOP principles.

### 1. Abstraction

`Book` is implemented as an **abstract class** containing common properties and behaviors shared by all books.

```java
public abstract class Book {
    ...
    public abstract String getBookType();
}
```

### 2. Encapsulation

Book properties such as ID, title, author, and borrowing status are kept private and accessed through public methods.

```java
private int bookId;
private String title;
private String author;
private boolean isBorrowed;
```

### 3. Inheritance

Specialized book classes inherit from the base `Book` class.

```text
           Book
          /    \
         /      \
AcademicBook   Storybook
```

### 4. Polymorphism

Different book types override methods such as `getBookType()` and `displayDetails()`.

The library stores different subclasses through a common reference type:

```java
ArrayList<Book> books;
```

This allows the system to treat different book types uniformly while preserving their specialized behavior.

### 5. Exception Handling

The application uses both Java's built-in exceptions and a custom exception:

```java
BookNotAvailableException
```

This is used when a user attempts an invalid book operation, such as borrowing an already borrowed book.

---

## 🏗️ Project Structure

```text
Library-Book-Management-System/
│
├── Library Book Management System/
│   ├── Book.java
│   ├── AcademicBook.java
│   ├── Storybook.java
│   ├── Library.java
│   ├── LibraryManagementSystem.java
│   └── BookNotAvailableException.java
│
├── .gitignore
├── LICENSE
└── README.md
```

### Class Responsibilities

| Class                       | Responsibility                                     |
| --------------------------- | -------------------------------------------------- |
| `Book`                      | Abstract base class for all books                  |
| `AcademicBook`              | Represents academic/reference books                |
| `Storybook`                 | Represents fiction/story books                     |
| `Library`                   | Manages the book collection and library operations |
| `LibraryManagementSystem`   | Provides the main menu and user interaction        |
| `BookNotAvailableException` | Handles invalid book availability operations       |

---

## 🔄 How It Works

When the application starts, it creates a `Library` object and loads a small set of sample books for demonstration.

The user is then presented with an interactive menu:

```text
╔════════════════════════════════════════════════════════════╗
║                  LIBRARY MANAGEMENT SYSTEM                 ║
╠════════════════════════════════════════════════════════════╣
║  [1] Add a New Book                                        ║
║  [2] Borrow a Book                                         ║
║  [3] Return a Book                                         ║
║  [4] Display All Books                                     ║
║  [5] Display Available Books                               ║
║  [6] Display Borrowed Books                                ║
║  [7] Exit                                                  ║
╚════════════════════════════════════════════════════════════╝
```

Books are stored in an `ArrayList<Book>` and can transition between:

```text
Available ──────► Borrowed
    ▲                 │
    └─────────────────┘
          Return
```

---

## 🛠️ Technologies & Concepts

### Technology

* **Java**

### Core Concepts

* Object-Oriented Programming
* Abstract Classes
* Inheritance
* Encapsulation
* Polymorphism
* Method Overriding
* Collections Framework
* `ArrayList`
* Exception Handling
* Custom Exceptions
* Input Validation
* Console-Based Application Development

---

## ▶️ Getting Started

### Prerequisites

Make sure Java is installed on your system.

Check your Java installation:

```bash
java -version
javac -version
```

### Clone the Repository

```bash
git clone https://github.com/tracolerd/Library-Book-Management-System.git
```

Navigate to the project directory:

```bash
cd Library-Book-Management-System
```

Then enter the source directory:

```bash
cd "Library Book Management System"
```

### Compile

```bash
javac *.java
```

### Run

```bash
java LibraryManagementSystem
```

---

## 🧪 Example Workflow

A typical session may look like:

```text
1. Start the application
2. View the available books
3. Borrow a book using its Book ID
4. View borrowed books
5. Return the borrowed book
6. Add a new academic book or storybook
7. Exit the application
```

---

## 📌 Current Scope

This project is intentionally designed as a **simple educational console application** focused on demonstrating Java and OOP concepts.

It currently uses in-memory data storage through `ArrayList`, so library data is not persisted after the application terminates.

It does not currently include:

* Database integration
* User authentication
* Admin/librarian accounts
* GUI or web interface
* Book reservations
* Due-date management
* Fine calculation
* Multi-user support

---

## 🚀 Possible Future Improvements

The system could be extended with:

* 💾 MySQL/PostgreSQL database integration
* 🔐 User authentication and role-based access
* 🔎 Book search and filtering
* 📅 Due dates and return deadlines
* 💰 Automatic fine calculation
* 🖥️ JavaFX/Swing graphical interface
* 🌐 REST API and web frontend
* 📊 Library statistics and reporting
* 👥 Separate librarian and member accounts

---

## 🎯 Learning Objectives

This project provides hands-on practice with:

1. Designing classes and relationships
2. Applying the four fundamental OOP principles
3. Using inheritance and method overriding
4. Managing objects with Java collections
5. Designing and handling custom exceptions
6. Building an interactive command-line application
7. Structuring a small Java application into multiple classes

---

## 👨‍💻 Author

**Tracolerd**

GitHub:
https://github.com/tracolerd

---

## 📄 License

This project is licensed under the **MIT License**. See the [`LICENSE`](LICENSE) file for details.
