namespace BookStore.Repository;
using BookStore.Data;
using BookStore.Models;

using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
	private readonly BookStoreContext _context;
    //Constructor injection
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
	
	public async Task<BooksModel> GetBookByIdAsync(int bookId)
	{
		var record = await _context.Books
			.Where(x => x.Id == bookId)
			.Select(x => new BooksModel()
			{
				Id = x.Id,
				Title = x.Title,
				Description = x.Description,
			}).FirstOrDefaultAsync();

		return record;
	}


	public async Task<int> AddBookAsync(BooksModel bookmodel)
	{
		var book = new Books()
		{
			Title = bookmodel.Title,
			Description = bookmodel.Description
		};

		_context.Books.Add(book);
		await _context.SaveChangesAsync();

		return book.Id;
    }


    // Updateing a book record

	public async Task<bool> UpdateBookAsync(int bookId, BooksModel bookmodel)
	{
        //var book = await _context.Books.FindAsync(bookId);
        //if(book!=null)
        //{
        //	book.Title = bookmodel.Title;
        //	book.Description= bookmodel.Description;

        //	await _context.SaveChangesAsync();
        //	return true;
        //}
        //return false;

        var book = new Books()
        {
			Id = bookId,
            Title = bookmodel.Title,
            Description = bookmodel.Description
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return true;
    }


}

