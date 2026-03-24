namespace it09_backend.DTOs
{
    public class CreateCommentRequestDto
    {
        public string AuthorName { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
}
