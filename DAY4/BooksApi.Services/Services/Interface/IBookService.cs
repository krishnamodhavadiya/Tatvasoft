using BooksApi.Entities.Entities;

namespace BooksApi.Services.Services.Interface
{
    public interface IBookService
    {
        Task InsertBook(BookDetails bookDetails);
        BookDetails GetBookDetailsById(int id);
        Task<List<BookDetails>> GetAll(); // ✅ make it async
 
                                          // Add this if your service uses it
    }
}
