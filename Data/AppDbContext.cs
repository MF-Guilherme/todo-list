using Microsoft.EntityFrameworkCore;
using Task = TodoList.Models.Task;

namespace TodoList.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Task> Tasks { get; set; }
}