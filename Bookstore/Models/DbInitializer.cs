using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public static class DbInitializer
    {
        public static void Initialize(BookstoreDbContext context)
        {
            // Drop and recreate the database
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any books
            if (context.Books.Any())
            {
                return;   // DB has been seeded
            }

            // Add authors
            var authors = new Author[]
            {
                new Author { FullName = "John Smith" },
                new Author { FullName = "Jane Doe" },
                new Author { FullName = "Robert Johnson" }
            };

            foreach (var author in authors)
            {
                context.Authors.Add(author);
            }
            context.SaveChanges();

            // Add books
            var books = new Book[]
            {
                new Book {
                    Title = "C# Programming",
                    Description = "A comprehensive guide to C# programming",
                    ImageUrl = "csharp.jpg",
                    PdfUrl = "csharp_guide.pdf",
                    Author = authors[0]
                },
                new Book {
                    Title = "ASP.NET Core MVC",
                    Description = "Learn web development with ASP.NET Core MVC",
                    ImageUrl = "aspnet.jpg",
                    PdfUrl = "aspnet_guide.pdf",
                    Author = authors[1]
                },
                new Book {
                    Title = "Entity Framework Core",
                    Description = "Database access with Entity Framework Core",
                    ImageUrl = "ef.jpg",
                    PdfUrl = null,
                    Author = authors[2]
                }
            };

            foreach (var book in books)
            {
                context.Books.Add(book);
            }
            context.SaveChanges();
        }
    }
}
