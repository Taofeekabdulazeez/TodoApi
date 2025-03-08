using TodoApi.Models;
using TodoApi.Dtos;

namespace TodoApi.Services;


public class TodosService(Supabase.Client supabase_client)
{
    private readonly Supabase.Client _client = supabase_client;

    public async Task<Todo> CreateTodo(CreateTodoDto CreateTodoDto)
    {
        var todo = new Todo
        {
            Title = CreateTodoDto.Title,
            Priority = CreateTodoDto.Priority,
            Assignee = CreateTodoDto.Assignee,
            DueDate = CreateTodoDto.DueDate,
            Status = CreateTodoDto.Status,
            Description = CreateTodoDto.Description,
        };

        var response = await _client.From<Todo>().Insert(todo);

        var newTodo = response.Models.First();

        return newTodo;
    }

    public async Task<List<Todo>> FindAllTodos()
    {
        var response = await _client.From<Todo>().Get();

        var todos = response.Models;

        return todos;
    }

    public async Task<Todo> FindTodoById(long id)
    {
        var response = await _client.From<Todo>().Where(t => t.Id == id).Get();

        var todo = response.Models.FirstOrDefault();

        return todo;
    }

    public async Task<Todo> UpdateTodoById(long id, UpdateTodoDto UpdateTodoDto)
    {
        var response = await _client.From<Todo>().Where(t => t.Id == id)
                .Set(t => t.Title, UpdateTodoDto.Title).Update();

        var updatedTodo = response.Models.FirstOrDefault();

        return updatedTodo;
    }

    public async Task DeleteTodoById(long id)
    {
        await _client.From<Todo>().Where(t => t.Id == id).Delete();
    }

}