namespace BookStore.Repository;
using BookStore.Data;
using BookStore.Models;

using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
        private readonly BookStoreContext _context;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<List<BooksModel>> GetAllBooksAsync()
        {
           var records = await _context.Books.Select(x => new BooksModel()
           {
               Id = x.Id,
               Title = x.Title,
               Description = x.Description,
           }).ToListAsync();

           return records;
        }
}

