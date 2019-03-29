using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class TodoController : ControllerBase {
    private readonly TodoContext _context;

    public TodoController(TodoContext context) {
      _context = context;

      if (_context.TodoItems.Count() == 0) {
        // Create a new TodoItem if collection is empty,
        // Which means you can't delete all TodoItems.
        _context.TodoItems.Add(new TodoItem { Name = "Default", IsComplete = "Nope" });
        _context.SaveChanges();
      }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems() {
      return await _context.TodoItems.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetTodo(long id) {
      TodoItem todoItem = await _context.TodoItems.FindAsync(id);

      if (todoItem == null) {
        return NotFound();
      }

      return todoItem;
    } 
  }
}