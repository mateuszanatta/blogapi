using Api.DTOs.Requests;
using Domain.Models;

namespace Api.Mappers.Request
{
    public static class BlogPostRequestMapper
    {
        public static BlogPost MapToDomain(this BlogPostRequest source)
        {
            return new BlogPost
            {
                Title = source.Title,
                Content = source.Content
            };
        }
    }
}
