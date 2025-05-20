using Core.Models;
using Core.Repositories;
using DAL.Helper;
using Microsoft.EntityFrameworkCore;
using static DAL.Helper.ArticleEntityTranslator;

namespace DAL.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DataContext _context;

        public ArticleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Article> CreateArticleAsync(Article article)
        {
            var articleEntity = ToEntity(article);

            _context.Articles.Add(articleEntity);
            await _context.SaveChangesAsync();

            // Laad de Source (eventueel opnieuw als je navigatie nodig hebt)
            await _context.Entry(articleEntity).Reference(e => e.Source).LoadAsync();

            return ToModel(articleEntity);
        }
    }
}
