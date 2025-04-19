namespace Api.DTOs.Response
{
    public record BlogPostResponse(Guid Id, string UserName, string Title, int TotalComments, string Content, IEnumerable<CommentResponse> Comments) : BlogPostSummaryResponse(Id, UserName, Title, TotalComments);
}
