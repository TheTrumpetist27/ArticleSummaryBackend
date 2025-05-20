
namespace Core.Services
{
    public interface ITextSummaryService
    {
        Task<string> SummarizeTextAsync(string content);
    }
}
