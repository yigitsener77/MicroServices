using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTful_PG.Data;

namespace RESTful_PG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoModelsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoModelsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoModel>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        // GET: api/TodoModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoModel>> GetTodoModel(int id)
        {
            var todoModel = await _context.Todos.FindAsync(id);

            if (todoModel == null)
            {
                return NotFound();
            }

            return todoModel;
        }

        // PUT: api/TodoModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoModel(int id, TodoModel todoModel)
        {
            if (id != todoModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoModel>> PostTodoModel(TodoModel todoModel)
        {
            _context.Todos.Add(todoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoModel", new { id = todoModel.Id }, todoModel);
        }

        // DELETE: api/TodoModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoModel(int id)
        {
            var todoModel = await _context.Todos.FindAsync(id);
            if (todoModel == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoModelExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }
    }
}
