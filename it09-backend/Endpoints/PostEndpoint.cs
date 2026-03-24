using it09_backend.DTOs;
using it09_backend.Services.Interfaces;

namespace it09_backend.Endpoints;

public static class PostEndpoints
{
    public static void MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/posts");

        group.MapGet("/{id:guid}", GetPostById);
        group.MapPost("/{id:guid}/comments", CreateComment);
        group.MapPost("/seed", SeedData);
    }

    private static async Task<IResult> GetPostById(Guid id, IPostService service)
    {
        var post = await service.GetPostByIdAsync(id);

        return post is null
            ? Results.NotFound(new { message = "Post not found." })
            : Results.Ok(post);
    }

    private static async Task<IResult> CreateComment(Guid id, CreateCommentRequestDto request, IPostService service)
    {
        try
        {
            var comment = await service.CreateCommentAsync(id, request);

            return comment is null
                ? Results.NotFound(new { message = "Post not found." })
                : Results.Ok(comment);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { message = ex.Message });
        }
    }

    private static async Task<IResult> SeedData(IPostService service)
    {
        var seeded = await service.SeedDataAsync();

        return Results.Ok(new
        {
            message = seeded ? "Seed completed successfully." : "Seed already exists."
        });
    }
}