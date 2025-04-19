namespace Domain.Models
{
    public class BlogPost
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public User User { get; set; }

        public ICollection<Comment> Comments { get; private set; } = [];

        public int TotalComments() => Comments.Count();

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }
    }
}
