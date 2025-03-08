
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Dtos;
using TodoApi.Responses;

namespace TodoApi.Controllers;

[Route("api/tasks")]
[ApiController]

public class TasksController(Supabase.Client supbase_client) : ControllerBase
{

    private readonly Supabase.Client _client = supbase_client;

    [HttpPost]

    public async Task<ActionResult<ApiResponse>> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        var task = new TaskModel
        {
            Title = createTaskDto.Title,
            Priority = createTaskDto.Priority,
            Assignee = createTaskDto.Assignee,
            DueDate = createTaskDto.DueDate,
            Status = createTaskDto.Status,
            Description = createTaskDto.Description,
        };

        var response = await _client.From<TaskModel>().Insert(task);

        var newTask = response.Models.First();

        return Ok(ApiResponse.Ok(data: newTask));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> GetTask()
    {
        var response = await _client.From<TaskModel>().Get();

        var Tasks = response.Models;

        return Ok(ApiResponse.Ok(data: Tasks));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> GetTask(long id)
    {
        var response = await _client.From<TaskModel>().Where(t => t.Id == id).Get();

        var Task = response.Models.FirstOrDefault();

        if (Task == null) return NotFound(ApiResponse.Notfound("Task not found!"));

        return Ok(ApiResponse.Ok(data: Task));
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<ApiResponse>> UpdateTask(long id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        var response = await _client.From<TaskModel>().Where(t => t.Id == id)
                .Set(t => t.Title, updateTaskDto.Title).Update();

        var updatedTask = response.Models.FirstOrDefault();

        return Ok(ApiResponse.Ok(data: updatedTask));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> DeleteTask(long id)
    {
        await _client.From<TaskModel>().Where(t => t.Id == id).Delete();

        return Ok(ApiResponse.Ok(data: null, message: "Task deleted successfully!"));
    }

}