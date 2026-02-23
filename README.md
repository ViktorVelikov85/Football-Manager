# Football Manager - Phase 2: Clubs Management

This application is a desktop solution for managing football clubs and players, developed using C# WinForms and MySQL. Phase 2 focuses on the core functionality for club data administration.

## Core Features

* **CRUD Operations**: Full support for Create, Read, Update, and Delete actions for football clubs.
* **Data Validation**: 
    * Mandatory field checks for club name and city.
    * Unique constraint handling to prevent duplicate club entries.
* **User Interface Enhancements**: 
    * Localized DataGridView headers.
    * Fixed-width ID columns and automatic row scaling.
    * Input clearing functionality for improved workflow.

## Technical Specifications

* **Language**: C# (.NET)
* **Database**: MySQL (MariaDB)
* **Driver**: MySql.Data
* **Architecture**: Repository Pattern for data access separation.

## Installation and Configuration

### 1. Database Setup
1. Ensure MySQL is running via XAMPP or a standalone service.
2. Create a database named `football_manager`.
3. Import the provided `football_manager.sql` file using phpMyAdmin or the MySQL command line to recreate the schema and initial data.

### 2. Application Setup
1. Open the solution file (`.sln`) in Microsoft Visual Studio.
2. Verify the connection string in the `Db.cs` class to ensure it matches your local database credentials (server, user, and password).
3. Build and run the project using F5.

## Project Structure
* `ClubsForm.cs`: Main interface for club management.
* `ClubsRepository.cs`: Data access logic for the clubs table.
* `Db.cs`: Centralized database connection and command execution helper.
* `clubs.sql`: Database export including table structures and unique constraints.

---
Developed by: Viktor Velikov
Date: February 2026
