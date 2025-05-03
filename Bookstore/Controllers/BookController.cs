using Bookstore.Models;
using Bookstore.Models.Repositories;
using Bookstore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepository<Book> bookRepository;
        private readonly IBookstoreRepository<Author> authorRepository;
        private readonly IWebHostEnvironment hosting;

        public BookController(IBookstoreRepository<Book> bookRepository,
            IBookstoreRepository<Author> authorRepository,
            IWebHostEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var book = bookRepository.List();
            return View(book);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);

            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = UploadFile(model.File) ?? string.Empty;
                    string pdfFileName = UploadFile(model.PdfFile, "pdfs") ?? string.Empty;

                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please Select an Author from the List";
                        return View(GetAllAuthors());
                    }
                    var author = authorRepository.Find(model.AuthorId);
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author,
                        ImageUrl = fileName,
                        PdfUrl = pdfFileName
                    };
                    bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("","You have to fill all the required fields !");
                return View(GetAllAuthors());
            }

        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);

            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var authorId = book.Author == null ? 0 : book.Author.Id;
            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Authors = authorRepository.List().ToList(),
                ImageUrl = book.ImageUrl,
                PdfUrl = book.PdfUrl
            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel viewModel)
        {
            try
            {
                string fileName = UploadFile(viewModel.File, viewModel.ImageUrl);
                string pdfFileName = UploadFile(viewModel.PdfFile, viewModel.PdfUrl, "pdfs");
                var author = authorRepository.Find(viewModel.AuthorId);
                Book book = new Book
                {
                    Id = viewModel.BookId,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Author = author,
                    ImageUrl = fileName,
                    PdfUrl = pdfFileName
                };
                bookRepository.Update(viewModel.BookId, book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);

            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        List<Author> FillSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "--- Please Select an Author ---" });
            return authors;
        }
        BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };

            return vmodel;
        }
        string UploadFile(IFormFile file, string subFolder = ""){
            if (file != null){
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");

                // If a subfolder is specified, create it if it doesn't exist
                if (!string.IsNullOrEmpty(subFolder))
                {
                    uploads = Path.Combine(uploads, subFolder);
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }
                }

                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));

                return file.FileName;
            }

            return null;
        }

        string UploadFile(IFormFile file, string existingUrl, string subFolder = ""){
            if (file != null){
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");

                // If a subfolder is specified, create it if it doesn't exist
                if (!string.IsNullOrEmpty(subFolder))
                {
                    uploads = Path.Combine(uploads, subFolder);
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }
                }

                string newPath = Path.Combine(uploads, file.FileName);

                if (!string.IsNullOrEmpty(existingUrl))
                {
                    string oldPath = Path.Combine(uploads, existingUrl);

                    if (oldPath != newPath && System.IO.File.Exists(oldPath)){
                        System.IO.File.Delete(oldPath);
                    }
                }

                file.CopyTo(new FileStream(newPath, FileMode.Create));
                return file.FileName;
            }
            return existingUrl;
        }



    }
}
