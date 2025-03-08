
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Dtos;
using TodoApi.Responses;
using TodoApi.Services;

namespace TodoApi.Controllers;

[Route("api/todos")]
[ApiController]

public class TodosController(TodosService todosService) : ControllerBase
{

    private readonly TodosService _todosService = todosService;

    [HttpPost]

    public async Task<ActionResult<ApiResponse>> CreateTodo([FromBody] CreateTodoDto CreateTodoDto)
    {

        var newTodo = await _todosService.CreateTodo(CreateTodoDto);

        return Ok(ApiResponse.Ok(data: newTodo));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetTask()
    {
        var todos = await _todosService.FindAllTodos();

        return Ok(ApiResponse.Ok(data: todos));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> GetTask(long id)
    {
        var todo = await _todosService.FindTodoById(id);

        if (todo == null) return NotFound(ApiResponse.Notfound("Todo not found!"));

        return Ok(ApiResponse.Ok(data: todo));
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<ApiResponse>> UpdateTodo(long id, [FromBody] UpdateTodoDto UpdateTodoDto)
    {
        var updatedTodo = await _todosService.UpdateTodoById(id, UpdateTodoDto);

        return Ok(ApiResponse.Ok(data: updatedTodo));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> DeleteTodo(long id)
    {
        await _todosService.DeleteTodoById(id);

        return Ok(ApiResponse.Ok(data: null, message: "Todo deleted successfully!"));
    }

}