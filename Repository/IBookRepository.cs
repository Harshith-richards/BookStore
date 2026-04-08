using BookStore.Models;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        Task<List<BooksModel>> GetAllBooksAsync();
        Task<BooksModel> GetBookByIdAsync(int bookId);

    }
}
