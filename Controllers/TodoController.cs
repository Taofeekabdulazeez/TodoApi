using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Dtos;
using TodoApi.Contexts;
using TodoApi.Responses;
using TodoApi.Pipelines;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController(TodoContext context) : ControllerBase
{
    private readonly TodoContext _context = context;

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetTodos()
    {
        List<TodoDTO> todos =  await _context.Todos
            .Select(x => ItemToDTO(x))
            .ToListAsync();

        return Ok(ApiResponse.Ok(data: todos));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> GetTodo(int id)
    {
        var Todo = await _context.Todos.FindAsync(id);

        if (Todo == null) return NotFound(ApiResponse.Notfound("Task not found!"));

        return Ok(ApiResponse.Ok(data: ItemToDTO(Todo)));
    }
  
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse>> PutTodo(int id, [FromBody] TodoDTO todoDTO)
    {
        if (id != todoDTO.Id) return BadRequest();

        var todo = await _context.Todos.FindAsync(id);

        if (todo == null) return NotFound(ApiResponse.Notfound(message: "Todo not found!"));

        todo.Name = todoDTO.Name;
        todo.IsComplete = todoDTO.IsComplete;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!TodoExists(id))
        {
            return NotFound(ApiResponse.Notfound(message: "Todo not found!"));
        }

        return Ok(ApiResponse.Ok(data: ItemToDTO(todo)));
    }


    [HttpPost]
    [MiddlewareFilter(typeof(LoggerMiddlewarePipeLine))]
    public async Task<ActionResult<ApiResponse>> PostTodo(TodoDTO todoDTO)
    {
        Todo todo = new()
        {
            Name = todoDTO.Name,
            IsComplete = todoDTO.IsComplete
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();


        return CreatedAtAction(
            nameof(GetTodo),
            new { id = todo.Id },
            ApiResponse.Created(data: ItemToDTO(todo)
           ));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> DeleteTodo(long id)
    {
        Todo? todo = await _context.Todos.FindAsync(id);

        if (todo == null) return NotFound(ApiResponse.Notfound(message: "Todo not found!"));

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return ApiResponse.Ok(message: "Todo successfully deleted!");
    }

    private bool TodoExists(int id)
    {
        return _context.Todos.Any(e => e.Id == id);
    }

    private static TodoDTO ItemToDTO(Todo Todo)
    {
        return new TodoDTO {
            Id = Todo.Id,
            Name = Todo.Name,
            IsComplete = Todo.IsComplete
        };
    }
}