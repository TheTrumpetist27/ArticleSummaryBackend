using Microsoft.AspNetCore.Mvc;
using Core.Services;
using API.DTOModels;
using static API.Helper.DTOTranslator;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> AddComment([FromBody] CreateCommentDTO createCommentDTO)
        {
            var comment = ToCommentModel(createCommentDTO);
            var createdComment = await _commentService.AddCommentAsync(comment);
            return Ok(ToCommentDTO(createdComment));
        }

        [HttpGet("{articleId:int}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsForArticle(int articleId)
        {
            var comments = await _commentService.GetCommentsForArticleAsync(articleId);
            var commentDTOs = comments.Select(ToCommentDTO).ToList();
            return Ok(commentDTOs);

        }
    }
}
