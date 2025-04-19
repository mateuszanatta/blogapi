using Domain.Models;

namespace Domain.Interfaces.Repositories;

public interface IBlogPostRepository
{
    Task<BlogPost> AddAsync(BlogPost blogPost);
    Task<List<BlogPost>> GetAllAsync();
    Task<BlogPost?> GetByIdAsync(Guid id);
    Task<Comment> AddCommentAsync(Comment comment);
    Task<BlogPost> UpdateAsync(BlogPost blogPost);
}

