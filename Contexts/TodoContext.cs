using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Contexts;

public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos { get; set; } = null!;
}