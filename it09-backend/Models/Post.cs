using System.Xml.Linq;

namespace it09_backend.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string AuthorName { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; } = default!;
        public List<Comment> Comments { get; set; } = new();
    }
}
