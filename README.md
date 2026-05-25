# Library Book Management System 📚

[cite_start]A console-based interactive Java application that simulates a small library management system[cite: 8, 10]. [cite_start]This project was designed and implemented as part of the **CSE110: Object-Oriented Programming** course at **East West University**[cite: 3, 5].

---

## 📌 Project Overview
[cite_start]The system provides a robust, menu-driven interface allowing users to manage a collection of books, handle borrowing/returning lifecycles, and dynamically filter book catalogs[cite: 11, 21]. [cite_start]The primary objective of this application is to demonstrate clean code standards and the practical application of core Object-Oriented Programming (OOP) principles in Java[cite: 12, 67].

---

## 🚀 Features
* [cite_start]**Menu-Driven Console Interface:** Continuous user interaction loop with structured numbered options and input validation[cite: 21, 22].
* [cite_start]**Dynamic Book Cataloging:** Supports adding new books under specialized categories (e.g., Academic Books or Storybooks)[cite: 24, 30].
* [cite_start]**Borrow & Return Lifecycle:** Allows users to borrow available titles by a unique ID and return them safely, updating the library state instantly[cite: 33, 35, 36, 39, 42].
* [cite_start]**Polymorphic Reporting:** Offers detailed list views filtering all books, only available titles, or currently borrowed assets using polymorphic method executions[cite: 44, 50, 51, 53].
* [cite_start]**In-Memory Storage:** Efficiently references all records in runtime using an `ArrayList<Book>` structure without heavy file dependencies[cite: 56, 57].

---

## 🛠️ OOP Concepts Demonstrated
* [cite_start]**Encapsulation:** All structural properties of books are kept strict and secure using `private` data fields, exposed safely via public getters and setters[cite: 14].
* [cite_start]**Inheritance:** Extended data models by creating specialized subclasses (like `Academic` and `Storybook`) branching from a core abstract base `Book` class[cite: 15, 30, 31].
* [cite_start]**Polymorphic Overriding:** Overrode baseline methods across subclasses to display dynamic, type-specific descriptions during runtime rendering[cite: 15, 50].
* [cite_start]**Defensive Exception Handling:** Built layered input and structural validations to intercept malformed console tokens, out-of-bound IDs, and operational faults like re-borrowing checked-out titles[cite: 23, 38, 60, 61, 62].
* [cite_start]**Custom Business Exceptions:** Implemented a target-specific custom runtime failure rule: `BookNotAvailableException`[cite: 63].

---

## 📂 System Architecture
[cite_start]The codebase strictly adheres to standard Java naming conventions and clean modular boundaries[cite: 68]:

```text
src/
├── Book.java                      # Base class containing abstract/shared fields (Title, Author, ID) [cite: 28, 29]
├── AcademicBook.java              # Subclass representing academic-specific titles [cite: 30, 31]
├── StoryBook.java                 # Subclass representing literary/storybook titles [cite: 30, 31]
├── Library.java                   # Core service class handling the ArrayList engine and rules [cite: 32, 56]
├── BookNotAvailableException.java # Custom exception checking asset availability status [cite: 63]
└── Main.java                      # Terminal application runner executing the menu loop [cite: 21, 22]
