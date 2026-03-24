namespace it09_backend.DTOs
{
    public class CommentResponseDto
    {
        public Guid Id { get; set; }
        public string AuthorName { get; set; } = default!;
        public string Message { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
