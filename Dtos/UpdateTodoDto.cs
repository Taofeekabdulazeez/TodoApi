namespace TodoApi.Dtos;

public class UpdateTodoDto
{
    public string? Title { get; set; }
    public string? Priority { get; set; }
    public string? Assignee { get; set; }
    public DateOnly? DueDate { get; set; }
    public string? Status { get; set; }
    public string? Description { get; set; }
}