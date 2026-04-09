using Azure;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        //Constructor injection
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        // GET: api/Books
        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var records = await _bookRepository.GetAllBooksAsync();
            return Ok(records);
        }
        
        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var record = await _bookRepository.GetBookByIdAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddBook(BooksModel bookmodel)
        {
            var id = await _bookRepository.AddBookAsync(bookmodel);
            return CreatedAtAction(nameof(GetBookById), new { id = id, Controller = "books" }, id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] BooksModel bookmodel , [FromRoute] int id)
        {

            bool result = await _bookRepository.UpdateBookAsync(id, bookmodel);
            if (result)
            {
                return CreatedAtAction(nameof(GetBookById), new { id = id, Controller = "books" }, id);
            }
            return NotFound(new { Message = $"No Book found in the id {id}"});
        }

        //[HttpPatch("{id}")]
        //public async Task<IActionResult> UpdateBookPatch([FromBody] JsonPatchDocument bookModel, [FromRoute] int id)
        //{

        //    bool result = await _bookRepository.UpdateBookPatchAsync(id, bookModel);
        //    if (result)
        //    {
        //        return CreatedAtAction(nameof(GetBookById), new { id = id, Controller = "books" }, id);
        //    }
        //    return NotFound(new { Message = $"No Book found in the id {id}" });
        //}


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return Ok();
        }

    }
}
