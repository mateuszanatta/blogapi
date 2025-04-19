namespace Domain.Models
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        public User User { get; set; }

        public string Content { get; set; } = string.Empty;

        public Guid BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }
    }
}