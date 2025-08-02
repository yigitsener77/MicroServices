using System.ComponentModel.DataAnnotations;

namespace Endpoint_SQLite.Data
{
    public abstract class Base
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class Book : Base
    {
        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string? ISBN { get; set; }
        public int? Pages { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
    }

    public class Author : Base
    {
        [Required]
        public required string Name { get; set; }
        public string? Bio { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public ICollection<Book>? Books { get; set; }
    }

    public class Publisher : Base
    {
        [Required]
        public required string Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
