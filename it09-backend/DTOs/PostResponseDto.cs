namespace it09_backend.DTOs
{
    public class PostResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string AuthorName { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; } = default!;
        public List<CommentResponseDto> Comments { get; set; } = new();
    }
}
