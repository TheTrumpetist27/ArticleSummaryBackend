using Microsoft.AspNetCore.Mvc;
using Core.Services;
using API.DTOModels;
using static API.Helper.DTOTranslator;
using API.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IHubContext<CommentHub> _hubContext;
        public CommentsController(ICommentService commentService, IHubContext<CommentHub> hubContext)
        {
            _commentService = commentService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> AddComment([FromBody] CreateCommentDTO createCommentDTO)
        {
            var comment = ToCommentModel(createCommentDTO);
            var createdComment = await _commentService.AddCommentAsync(comment);
            var createdDTO = ToCommentDTO(createdComment);

            // SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveComment", createdDTO);

            return Ok(createdDTO);
        }

        [HttpGet("{articleId:int}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsForArticle(int articleId)
        {
            var comments = await _commentService.GetCommentsForArticleAsync(articleId);
            var commentDTOs = comments.Select(ToCommentDTO).ToList();
            return Ok(commentDTOs);

        }

        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var result = await _commentService.DeleteCommentAsync(commentId);
            if (result)
            {
                return NoContent(); // 204
            }
            return NotFound(); // 404
        }
    }
}
