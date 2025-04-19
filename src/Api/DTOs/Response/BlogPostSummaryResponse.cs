namespace Api.DTOs.Response
{
    public record BlogPostSummaryResponse(Guid Id, string UserName, string Title, int TotalComments);
}
