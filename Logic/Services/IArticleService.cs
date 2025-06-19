using Core.Models;

namespace Core.Services
{
    public interface IArticleService
    {
        Task<Article> CreateArticleAsync(Article article);
        Task<IEnumerable<Article>> GetAllArticles();
        Task<Article?> GetArticleById(int id);
        Task<Article> UpdateArticleAsync(Article article);
        Task<int> DeleteArticle(int id);
    }
}
