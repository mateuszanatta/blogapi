using Api.DTOs.Response;
using Domain.Models;

namespace Api.Mappers.Domain
{
    public static class CommentMapper
    {
        public static CommentResponse MapToResponse(this Comment source)
        {
            return new CommentResponse(source?.User?.Name ?? string.Empty, source.Content);
        }

        public static IEnumerable<CommentResponse> MapToResponse(this IEnumerable<Comment> source)
        {
            return source.Select(MapToResponse);
        }
    }
}
