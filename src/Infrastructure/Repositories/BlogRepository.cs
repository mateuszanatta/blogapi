using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BlogRepository : IBlogPostRepository
    {
        private readonly BlogDbContext _context;

        public BlogRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            var savedPost = _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();

            return savedPost.Entity;
        }

        public Task<List<BlogPost>> GetAllAsync()
        {
            return _context.BlogPosts
                .Include(post => post.User)
                .Include(post => post.Comments).ToListAsync();
        }

        public Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return _context.BlogPosts
                .Include(post => post.User)
                .Include(post => post.Comments).FirstOrDefaultAsync(post => post.Id.Equals(id));
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var savedPost = _context.BlogPosts.Update(blogPost);
            await _context.SaveChangesAsync();

            return savedPost.Entity;
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            var savedComment = _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return savedComment.Entity;            
        }
    }
}
