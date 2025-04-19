using Api.DTOs.Requests;
using Domain.Models;

namespace Api.Mappers.Request
{
    public static class CommentRequestMapper
    {
        public static Comment MapToDomain(this CommentRequest source, Guid postId, Guid userId)
        {
            return new Comment
            {
                BlogPostId = postId,
                UserId = userId,
                Content = source.Content
            };
        }
    }
}
