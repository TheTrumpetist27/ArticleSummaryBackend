using Core.Models;
using Core.Repositories;
using System.Runtime.InteropServices;

namespace Core.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;
        private readonly ITextSummaryService _textSummaryService;
        public ArticleService(IArticleRepository repository, ITextSummaryService textSummaryService)
        {
            _repository = repository;
            _textSummaryService = textSummaryService;
        }

        public async Task<Article> CreateArticleAsync(Article article)
        {
            article.Summary = await _textSummaryService.SummarizeTextAsync(article.Source.Content);
            return await _repository.CreateArticleAsync(article);
        }

        public async Task<IEnumerable<Article>> GetAllArticles()
        {
            return await _repository.GetAllArticles();
        }

        public async Task<Article?> GetArticleById(int id)
        {
            return await _repository.GetArticleById(id);
        }
        public async Task<int> DeleteArticle(int id)
        {
            return await _repository.DeleteArticle(id);
        }
        //public async Task<int> UpdateArticle(Article article)
        //{
        //    return await _repository.UpdateArticle(article);
        //}
    }
}
