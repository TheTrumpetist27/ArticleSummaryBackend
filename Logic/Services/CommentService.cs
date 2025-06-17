using Core.Models;
using Core.Repositories;

namespace Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;

        public CommentService(ICommentRepository repository)
        {
            _repository = repository;
        }
        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            return await _repository.AddCommentAsync(comment);
        }
        public async Task<List<Comment>> GetCommentsForArticleAsync(int articleId)
        {
            return await _repository.GetCommentsForArticleAsync(articleId);
        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            return await _repository.DeleteCommentAsync(commentId);
        }
    }
}
