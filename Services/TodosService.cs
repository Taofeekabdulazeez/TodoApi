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

    public async Task<Todo> UpdateTodoById(long id, UpdateTodoDto updateTodoDto)
    {
        var query = _client.From<Todo>().Where(t => t.Id == id);

        if (!string.IsNullOrEmpty(updateTodoDto.Title))
            query = query.Set(t => t.Title, updateTodoDto.Title);

        if (!string.IsNullOrEmpty(updateTodoDto.Priority))
            query = query.Set(t => t.Priority, updateTodoDto.Priority);

        if (!string.IsNullOrEmpty(updateTodoDto.Assignee))
            query = query.Set(t => t.Assignee, updateTodoDto.Assignee);

        if (!string.IsNullOrEmpty(updateTodoDto.Status))
            query = query.Set(t => t.Status, updateTodoDto.Status);

        if (!string.IsNullOrEmpty(updateTodoDto.Description))
            query = query.Set(t => t.Description, updateTodoDto.Description);

        if (updateTodoDto.DueDate.HasValue)  // If DueDate is nullable
            query = query.Set(t => t.DueDate, updateTodoDto.DueDate.Value);

        var response = await query.Update();

        var updatedTodo = response.Models.FirstOrDefault();

        return updatedTodo;
    }

    public async Task DeleteTodoById(long id)
    {
        await _client.From<Todo>().Where(t => t.Id == id).Delete();
    }

}