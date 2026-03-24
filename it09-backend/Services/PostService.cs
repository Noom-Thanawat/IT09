using it09_backend.Data;
using it09_backend.DTOs;
using it09_backend.Models;
using it09_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace it09_backend.Services;

public class PostService : IPostService
{
    private readonly AppDbContext _db;
    private static readonly Guid SeedPostId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    public PostService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PostResponseDto?> GetPostByIdAsync(Guid id)
    {
        var post = await _db.Posts
            .AsNoTracking()
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (post is null)
            return null;

        return new PostResponseDto
        {
            Id = post.Id,
            Title = post.Title,
            AuthorName = post.AuthorName,
            CreatedAt = post.CreatedAt,
            ImageUrl = post.ImageUrl,
            Comments = post.Comments
                .OrderBy(c => c.CreatedAt)
                .Select(c => new CommentResponseDto
                {
                    Id = c.Id,
                    AuthorName = c.AuthorName,
                    Message = c.Message,
                    CreatedAt = c.CreatedAt
                })
                .ToList()
        };
    }

    public async Task<CommentResponseDto?> CreateCommentAsync(Guid postId, CreateCommentRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.AuthorName))
            throw new ArgumentException("AuthorName is required.");

        if (string.IsNullOrWhiteSpace(request.Message))
            throw new ArgumentException("Message is required.");

        var postExists = await _db.Posts.AnyAsync(x => x.Id == postId);
        if (!postExists)
            return null;

        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            PostId = postId,
            AuthorName = request.AuthorName.Trim(),
            Message = request.Message.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        _db.Comments.Add(comment);
        await _db.SaveChangesAsync();

        return new CommentResponseDto
        {
            Id = comment.Id,
            AuthorName = comment.AuthorName,
            Message = comment.Message,
            CreatedAt = comment.CreatedAt
        };
    }

    public async Task<bool> SeedDataAsync()
    {
        var existingPost = await _db.Posts.FirstOrDefaultAsync(x => x.Id == SeedPostId);
        if (existingPost is not null)
            return false;

        var now = DateTime.UtcNow;

        var post = new Post
        {
            Id = SeedPostId,
            Title = "IT 08-1",
            AuthorName = "Thanawat Sangrod",
            CreatedAt = now,
            ImageUrl = "https://images.unsplash.com/photo-1504384308090-c894fdcc538d?auto=format&fit=crop&w=900&q=80"
        };

        var comments = new List<Comment>
        {
            new Comment
            {
                Id = Guid.NewGuid(),
                PostId = SeedPostId,
                AuthorName = "Blend 285",
                Message = "have a good day",
                CreatedAt = now.AddMinutes(-3)
            }
        };

        _db.Posts.Add(post);
        _db.Comments.AddRange(comments);

        await _db.SaveChangesAsync();
        return true;
    }
}