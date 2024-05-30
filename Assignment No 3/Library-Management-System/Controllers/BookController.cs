using Library_Management_System.Entities;
using Library_Management_System.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

using System.Xml.Linq;

namespace Library_Management_System.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]

    public class BookController : Controller
    {

        public Container Container;

        public BookController()
        {
            Container = GetContainer();
        }

        private Container GetContainer()
        {
            string URI = "https://localhost:8081";
            string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
            string DatabaseName = "LibraryDB";
            string ContainerName = "Books";

            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container= database.GetContainer(ContainerName);

            return container;
        }


        [HttpPost]
        public async Task<BookModel> AddBook(BookModel bookModel)
        {

            
            BookEntity book = new BookEntity
            {
                Title = bookModel.Title,
                Author = bookModel.Author,
                PublishedDate = bookModel.PublishedDate,
                ISBN = bookModel.ISBN,
                IsIssued = bookModel.IsIssued,
                Id = Guid.NewGuid().ToString(),
                UId = Guid.NewGuid().ToString(),
                DocumentType = "book",
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now,
                UpdatedBy = "Admin",
                UpdatedOn = DateTime.Now,
                Version = 1,
                Active = true,
                Archived = false
            };

            
            BookEntity response = await Container.CreateItemAsync(book);

            BookModel responseModel = new BookModel
            {
                UId = response.UId,
                Title = response.Title,
                Author = response.Author,
                PublishedDate = response.PublishedDate,
                ISBN = response.ISBN,
                IsIssued = response.IsIssued
            };
            return responseModel;
        }

        [HttpGet]
        public async Task<BookModel> GetBookByUId(string UId)
        {

            var book = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == UId && q.Active == true && q.Archived == false).FirstOrDefault();

            BookModel bookModel = new BookModel
            {
                UId = book.UId,
                Title = book.Title,
                Author = book.Author,
                PublishedDate = book.PublishedDate,
                ISBN = book.ISBN,
                IsIssued = book.IsIssued
            };

             return bookModel;

        }

        [HttpGet]
        public async Task<BookModel> GetBookByName(string name)
        {

            var book = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Title == name && q.Active == true && q.Archived == false).FirstOrDefault();


            BookModel bookModel = new BookModel
            {
                UId = book.UId,
                Title = book.Title,
                Author = book.Author,
                PublishedDate = book.PublishedDate,
                ISBN = book.ISBN,
                IsIssued = book.IsIssued
            };

            return bookModel;
        }

        [HttpGet]
        public async Task<List<BookModel>> GetAllBooks()
        {

            var books = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType == "book").ToList();

            List<BookModel> bookModels = new List<BookModel>();

            foreach (var book in books)
            {
                BookModel model = new BookModel
                {
                    UId = book.UId,
                    Title = book.Title,
                    Author = book.Author,
                    PublishedDate = book.PublishedDate,
                    ISBN = book.ISBN,
                    IsIssued = book.IsIssued
                };
                bookModels.Add(model);
            }

            return bookModels;

        }

        [HttpGet]
        public async Task<List<BookModel>> GetAvailableBooks()
        {

            var books = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.IsIssued == false && q.Active == true && q.Archived == false && q.DocumentType == "book").ToList();

            List<BookModel> bookModels = new List<BookModel>();

            foreach (var book in books)
            {
                BookModel model = new BookModel
                {
                    UId = book.UId,
                    Title = book.Title,
                    Author = book.Author,
                    PublishedDate = book.PublishedDate,
                    ISBN = book.ISBN,
                    IsIssued = book.IsIssued
                };
                bookModels.Add(model);
            }

            return bookModels;

        }

        [HttpGet]
        public async Task<List<BookModel>> GetIssuedBooks()
        {

            var books = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.IsIssued == true && q.Active == true && q.Archived == false && q.DocumentType == "book").ToList();

            List<BookModel> bookModels = new List<BookModel>();

            foreach (var book in books)
            {
                BookModel model = new BookModel
                {
                    UId = book.UId,
                    Title = book.Title,
                    Author = book.Author,
                    PublishedDate = book.PublishedDate,
                    ISBN = book.ISBN,
                    IsIssued = book.IsIssued
                };
                bookModels.Add(model);
            }

            return bookModels;
        }

        [HttpPost]
        public async Task<BookModel> UpdateBook(BookModel book)
        {

            var existingBook = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == book.UId && q.Active == true && q.Archived == false).FirstOrDefault();

            existingBook.Archived = true;
            existingBook.Active = false;
            await Container.ReplaceItemAsync(existingBook, existingBook.Id);

           
            existingBook.Id = Guid.NewGuid().ToString();
            existingBook.UpdatedBy = "Admin";
            existingBook.UpdatedOn = DateTime.Now;
            existingBook.Version = existingBook.Version + 1;
            existingBook.Active = true;
            existingBook.Archived = false;

            
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublishedDate = book.PublishedDate;
            existingBook.ISBN = book.ISBN;
            existingBook.IsIssued = book.IsIssued;

            existingBook = await Container.CreateItemAsync(existingBook);

            BookModel response = new BookModel
            {
                UId = existingBook.UId,
                Title = existingBook.Title,
                Author = existingBook.Author,
                PublishedDate = existingBook.PublishedDate,
                ISBN = existingBook.ISBN,
                IsIssued = existingBook.IsIssued
            };

            return response;
        }



    }
}
