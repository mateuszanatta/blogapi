using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IBlogPostService
    {
        Task<BlogPost> CreatePostAsync(BlogPost blogPost, Guid userId);
        Task<Comment> AddCommentAsync(Guid id, Comment comment, Guid userId);
        Task<List<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetBlogPostById(Guid id);
    }
}
