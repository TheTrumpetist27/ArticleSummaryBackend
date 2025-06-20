﻿using Microsoft.AspNetCore.Mvc;
using Core.Services;
using API.DTOModels;
using static API.Helper.DTOTranslator;
using API.Hubs;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> AddComment([FromBody] CreateCommentDTO createCommentDTO)
        {
            var comment = ToCommentModel(createCommentDTO);
            var createdComment = await _commentService.AddCommentAsync(comment);
            var createdDTO = ToCommentDTO(createdComment);
            return Ok(createdDTO);
        }

        [HttpGet("{articleId:int}")]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsForArticle(int articleId)
        {
            var comments = await _commentService.GetCommentsForArticleAsync(articleId);
            var commentDTOs = comments.Select(ToCommentDTO).ToList();
            return Ok(commentDTOs);

        }

        [Authorize]
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
