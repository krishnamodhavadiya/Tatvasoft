using BooksApi.Entities.Context;
using BooksApi.Entities.Entities;
using BooksApi.Entities.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Entities.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _context;

        public BookRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task InsertBook(BookDetails bookDetails)
        {
            await _context.BookDetails.AddAsync(bookDetails);
            await _context.SaveChangesAsync();
        }

        public BookDetails GetById(int id)
        {
            return _context.BookDetails.FirstOrDefault(b => b.Id == id);
        }

        public async Task<List<BookDetails>> GetAll()
        {
            return await _context.BookDetails.ToListAsync();
        }
    }
}
