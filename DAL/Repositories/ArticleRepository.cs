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

        public async Task<IEnumerable<Article>> GetAllArticles()
        {
            return await _context.Articles
                .Include(a => a.Source)
                .Select(a => ToModel(a))
                .ToListAsync();
        }

        public async Task<Article?> GetArticleById(int id)
        {
            var articleEntity = await _context.Articles
                .Include(a => a.Source)
                .FirstOrDefaultAsync(a => a.Id == id);
            return articleEntity != null ? ToModel(articleEntity) : null;
        }

        public async Task<int> DeleteArticle(int id)
        {
            var articleEntity = await _context.Articles.FindAsync(id);
            if (articleEntity == null)
            {
                return 0; // Artikel niet gevonden
            }
            _context.Articles.Remove(articleEntity);
            return await _context.SaveChangesAsync();
        }

        public async Task<Article> UpdateArticleAsync(Article article)
        {
            var entity = await _context.Articles.FindAsync(article.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Article with ID {article.Id} not found.");
            }
            entity = ToEntity(article);

            await _context.SaveChangesAsync();
            return ToModel(entity);
        }
    }
}
