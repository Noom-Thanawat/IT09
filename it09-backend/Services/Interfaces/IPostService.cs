using it09_backend.DTOs;

namespace it09_backend.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostResponseDto?> GetPostByIdAsync(Guid id);
        Task<CommentResponseDto?> CreateCommentAsync(Guid postId, CreateCommentRequestDto request);
        Task<bool> SeedDataAsync();
    }
}
