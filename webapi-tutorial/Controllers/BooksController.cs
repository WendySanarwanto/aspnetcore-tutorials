using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebapiTutorial.Models;
using WebapiTutorial.Services;

namespace WebapiTutorial.Controllers {

  [Route("api/[controller]")]
  [ApiController]
  public class BooksController : ControllerBase
  {
    private readonly BookService _bookService;

    public BooksController(BookService bookService) {
      this._bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks() {
      return await this._bookService.Get();
    }

    [HttpGet("{id:length(24)}", Name = "GetBook")]
    public ActionResult<Book> GetBook(string id) {
      var book = this._bookService.Get(id);
      if (book == null) {
        return NotFound();
      }

      return book;
    }

    [HttpPost]
    public ActionResult<Book> CreateNewBook(Book newBook) {
      var createdBook = this._bookService.Create(newBook);
      return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id.ToString() }, createdBook);
    }

    [HttpPut("{id:length(24)}")]
    public ActionResult<Book> UpdateBook(string id, Book changedBook) {
      var foundBook = this._bookService.Get(id);
      if (foundBook == null) {
        return NotFound();
      }

      this._bookService.Update(id, changedBook);

      return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public ActionResult<Book> DeleteBook(string id) {
      var foundBook = this._bookService.Get(id);
      if (foundBook == null) {
        return NotFound();
      }

      this._bookService.Remove(foundBook);

      return NoContent();
    }
  }
}