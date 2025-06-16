using Core.Models;
using Core.Repositories;
using DAL.Helper;
using Microsoft.EntityFrameworkCore;
using static DAL.Helper.CommentEntityTranslator;

namespace DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            var commentEntity = CommentEntityTranslator.ToEntity(comment);
            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();
            // Laad de Article (eventueel opnieuw als je navigatie nodig hebt)
            await _context.Entry(commentEntity).Reference(e => e.Article).LoadAsync();
            return CommentEntityTranslator.ToModel(commentEntity);
        }
        public async Task<List<Comment>> GetCommentsForArticleAsync(int articleId)
        {
            return await _context.Comments
                .Where(c => c.ArticleId == articleId)
                .Select(c => CommentEntityTranslator.ToModel(c))
                .ToListAsync();
        }
    }
}
