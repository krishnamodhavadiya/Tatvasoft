using BooksApi.Models;
using BooksApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly BookService bookService;

        public BookController(BookService book) 
        {
            this.bookService = book;
        }

        [HttpPost]
        [Route("Add")]
        public ActionResult AddBook(Book book)
        {
            this.bookService.AddBook(book);
            return Ok("Book created !");

        }


        [HttpGet]
        [Route("GetAll")]
        public ActionResult GetAll()
        {
            return Ok(this.bookService.GetAll());
        }

        [HttpGet]
        [Route("GetById")]
        public ActionResult GetById(int id)
        {
            var res = this.bookService.GetBookById(id);

            if (res != null) { return Ok(res); }

            return NotFound("Book not found!");
        }

        // To Update Book

        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateBook([FromBody] Book updatedBook)
        {
            var existingBook = this.bookService.GetBookById(updatedBook.Id);

            if (existingBook == null)
            {
                return NotFound("Book not found!");
            }

            this.bookService.UpdateBook(updatedBook);
            return Ok("Book updated successfully!");
        }
        // To Delete Book


        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteBook(int id)
        {
            var existingBook = this.bookService.GetBookById(id);

            if (existingBook == null)
            {
                return NotFound("Book not found!");
            }

            this.bookService.DeleteBook(id);
            return Ok("Book deleted successfully!");
        }
    }
}
