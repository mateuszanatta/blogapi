using Api.DTOs.Response;
using Domain.Models;

namespace Api.Mappers.Domain
{
    public static class BlogPostMapper
    {
        public static BlogPostSummaryResponse MapToSummaryResponse(this BlogPost source)
        {
            return new BlogPostSummaryResponse(source.Id, source.User.Name, source.Title, source.TotalComments());
        }

        public static IEnumerable<BlogPostSummaryResponse> MapToSummaryResponse(this IEnumerable<BlogPost> source)
        {
            return source.Select(MapToSummaryResponse);
        }

        public static BlogPostResponse MapToResponse(this BlogPost source)
        {
            return new BlogPostResponse(source.Id, source.User.Name, source.Title, source.TotalComments(), source.Content, source.Comments.MapToResponse());
        }
    }
}
