using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace TodoApi.Models;

[Table("todo")]
public class Todo : BaseModel
{
    [PrimaryKey("id", false)]
    public long Id { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("priority")]
    public string Priority { get; set; }

    [Column("assignee")]
    public string Assignee { get; set; }

    [Column("due_date")]
    public string DueDate { get; set; }

    [Column("status")]
    public string Status { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}

