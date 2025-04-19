using Api.DTOs.Response;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Mappers.Domain;
using Api.DTOs.Requests;
using Api.Mappers.Request;
using System.Security.Claims;
using Domain.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EndpointSummary("Return a list of all blog posts")]
        public async Task<ActionResult<List<BlogPostSummaryResponse>>> GetBlogPostsSummaryAsync()
        {
            var blogPosts = await _blogPostService.GetAllAsync();

            if (blogPosts is null) return NotFound();

            return Ok(blogPosts.MapToSummaryResponse());
        }

        [HttpPost]
        [Authorize]
        [EndpointSummary("Create a new blog post")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BlogPostResponse))]
        public async Task<ActionResult<BlogPostResponse>> CreateBlogPostAsync([FromBody] BlogPostRequest blogPostRequest)
        {
            var blogPost = blogPostRequest.MapToDomain();
            var userId = GetUserIdFromJwt();
            var createdBlogPost = await _blogPostService.CreatePostAsync(blogPost, userId);
            var response = createdBlogPost.MapToResponse();
            return CreatedAtAction(nameof(GetBlogPostByIdAsync), new { id = response.Id}, response);
        }

        [Authorize]
        [HttpGet("{id}", Name = nameof(GetBlogPostByIdAsync))]
        [ActionName(nameof(GetBlogPostByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlogPostResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Retrieve a specific blog post by its ID")]
        public async Task<ActionResult<BlogPostResponse>> GetBlogPostByIdAsync(Guid id)
        {
            var blogPost = await _blogPostService.GetBlogPostById(id);
            if(blogPost is null) return NotFound();

            return Ok(blogPost.MapToResponse());
        }

        [Authorize]
        [HttpPost("{id:guid}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EndpointSummary("Add a new comment to a specific blog post")]
        public async Task<ActionResult<List<Comment>>> CreateCommentToBlogPost(Guid id, [FromBody] CommentRequest commentRequest)
        {
            var userId = GetUserIdFromJwt();
            var comment = commentRequest.MapToDomain(id, userId);
            var savedComment = await _blogPostService.AddCommentAsync(id, comment, userId);
            return Ok(savedComment.MapToResponse());
        }

        private Guid GetUserIdFromJwt()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userIdClaim);
        }
    }
}
