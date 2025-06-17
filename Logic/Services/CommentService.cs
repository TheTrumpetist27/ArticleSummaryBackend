using Core.Models;
using Core.Repositories;

namespace Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly ICommentBroadcastService _broadcastService;

        public CommentService(ICommentRepository repository, ICommentBroadcastService broadcastService)
        {
            _repository = repository;
            _broadcastService = broadcastService;
        }
        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            var created = await _repository.AddCommentAsync(comment);
            await _broadcastService.BroadcastComment(created);
            return created;
        }
        public async Task<List<Comment>> GetCommentsForArticleAsync(int articleId)
        {
            return await _repository.GetCommentsForArticleAsync(articleId);
        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            var success = await _repository.DeleteCommentAsync(commentId);
            if (success)
            {
                await _broadcastService.BroadcastDelete(commentId);
            }
            return success;
        }
    }
}
