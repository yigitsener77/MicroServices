using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RESTful_PG.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TodoModel> Todos { get; set; }
    }

    public class TodoModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Content { get; set; }
        public bool IsComplete { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
