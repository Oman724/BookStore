# Bookstore Application

A modern ASP.NET Core MVC application for managing a bookstore's inventory, including books and authors.

## Features

- **Book Management**: Add, edit, delete, and view books
- **Author Management**: Add, edit, delete, and view authors
- **PDF Support**: Upload and download PDF files for books
- **Image Support**: Upload and display book cover images
- **Responsive Design**: Modern UI that works on desktop and mobile devices

## Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- Bootstrap 4
- SQL Server (LocalDB)
- HTML5/CSS3
- JavaScript/jQuery

## Requirements

- .NET 8.0 SDK or later
- Visual Studio 2022 or Visual Studio Code
- SQL Server LocalDB (included with Visual Studio)

## Getting Started

### Installation

1. Clone the repository
   ```
   git clone https://github.com/yourusername/Bookstore_ASP.NETCore.git
   ```

2. Navigate to the project directory
   ```
   cd Bookstore_ASP.NETCore
   ```

3. Restore dependencies
   ```
   dotnet restore
   ```

4. Run the application
   ```
   dotnet run --project Bookstore
   ```

5. Open your browser and navigate to:
   ```
   http://localhost:5000
   ```

### Database

The application will automatically create and seed the database on first run. If you need to reset the database, simply delete the database file and restart the application.

## Project Structure

- **Controllers/**: Contains MVC controllers
- **Models/**: Contains data models and database context
- **Views/**: Contains Razor views
- **wwwroot/**: Contains static files (CSS, JS, images)
- **ViewModels/**: Contains view models for form handling

## Features in Detail

### Book Management
- View all books with cover images
- Add new books with title, description, author, cover image, and PDF
- Edit existing books
- Delete books
- View detailed information about each book

### Author Management
- View all authors
- Add new authors
- Edit existing authors
- Delete authors
- View books by author

### PDF Support
- Upload PDF files for books
- Download PDF files from book details page
- PDF indicator on book cards

## Acknowledgments

- Bootstrap for the responsive design
- Font Awesome for the icons
- ASP.NET Core team for the amazing framework