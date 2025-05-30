using BooksApi.Entities.Entities;
using BooksApi.Entities.Repositories.Interface;
using BooksApi.Models;
using BooksApi.Services.Services.Interface;

namespace BooksApi.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task InsertBook(BookDetails bookDetails)
        {
            await _bookRepository.InsertBook(bookDetails);
        }

        public BookDetails GetBookDetailsById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public async Task<List<BookDetails>> GetAll()
        {
            return await _bookRepository.GetAll();
        }


    }
}
