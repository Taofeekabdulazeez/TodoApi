using TodoApi.Validations;

namespace TodoApi.Dtos;

public class TodoDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "name is required")]
    [MinLength(2)]
    public string? Name { get; set; } = string.Empty;

    [MustBeFalse(ErrorMessage = "You cannot create a completed todo")]
    public bool IsComplete { get; set; } = false;

    public override string ToString() => $"Id: {Id}, Name: {Name}, IsComplete: {IsComplete}";
}