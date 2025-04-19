using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Ganss.Xss;

namespace Domain.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHtmlSanitizer _htmlSanitizer;

        public BlogPostService(IBlogPostRepository blogPostRepository, IUserRepository userRepository, IHtmlSanitizer htmlSanitizer)
        {
            _blogPostRepository = blogPostRepository;
            _userRepository = userRepository;
            _htmlSanitizer = htmlSanitizer;
        }

        public async Task<Comment> AddCommentAsync(Guid postId, Comment comment, Guid userId)
        {
            var blogPost = _blogPostRepository.GetByIdAsync(comment.BlogPostId);
            var user = _userRepository.GetAsync(comment.UserId);
            await Task.WhenAll(blogPost, user);

            var blogPostResult = blogPost.Result;
            var userResult = user.Result;

            if (blogPostResult is null)
            {
                throw new NotFoundException("Blog Post does not exists");
            }

            if (userResult is null)
            {
                throw new NotFoundException("User does not exists");
            }

            comment.Content = _htmlSanitizer.Sanitize(comment.Content);
            comment.BlogPost = blogPostResult;
            comment.User = userResult;
            return await _blogPostRepository.AddCommentAsync(comment);
        }

        public async Task<BlogPost> CreatePostAsync(BlogPost blogPost, Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user is null)
            {
                throw new NotFoundException("User does not exists");
            }

            blogPost.Content = _htmlSanitizer.Sanitize(blogPost.Content);
            blogPost.User = user;
            return await _blogPostRepository.AddAsync(blogPost);
        }

        public Task<List<BlogPost>> GetAllAsync()
        {
            return _blogPostRepository.GetAllAsync();
        }

        public Task<BlogPost?> GetBlogPostById(Guid id)
        {
            return _blogPostRepository.GetByIdAsync(id);
        }
    }
}
