namespace BookStore.Repository;

using AutoMapper;
using Azure;
using BookStore.Data;
using BookStore.Models;

using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
	private readonly BookStoreContext _context;
    private readonly IMapper _mapper;
    //Constructor injection
    public BookRepository(BookStoreContext context, IMapper mapper)
	{
		_context = context;
        _mapper = mapper;
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
		//var record = await _context.Books
		//	.Where(x => x.Id == bookId)
		//	.Select(x => new BooksModel()
		//	{
		//		Id = x.Id,
		//		Title = x.Title,
		//		Description = x.Description,
		//	}).FirstOrDefaultAsync();

		//return record;

		var book = await _context.Books.FindAsync(bookId);
		return _mapper.Map<BooksModel>(book);


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

	//public async Task<bool> UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel)
	//{
	//	var book = await _context.Books.FindAsync(bookId);
	//	if (book != null)
	//	{
	//		 bookModel.ApplyTo(book);
	//		await _context.SaveChangesAsync();
 //           return true;
 //       }
 //       return false;
	//}

	public async Task DeleteBookAsync(int bookId)
	{
		var book = new Books() { Id= bookId };
		_context.Books.Remove(book);
		await _context.SaveChangesAsync();
    }


}

