using Core.Models;

namespace Core.Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllArticles();
        Task<Article> GetArticleById(int id);
        Task<Article> CreateArticleAsync(Article article);
        //Task<int> UpdateArticle(Article article);
        //Task<int> DeleteArticle(int id);
    }
}
