namespace TodoApi.Dtos
{
    public class CreateTodoDto
    {
        public required string Title { get; set; }
        public required string Priority { get; set; }
        public required string Assignee { get; set; }
        public required string DueDate { get; set; }
        public required string Status { get; set; }
        public required string Description { get; set; }
    }
}