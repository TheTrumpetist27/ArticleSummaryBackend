using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Core.Models;
using API.DTOModels;
using static API.Helper.DTOTranslator;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ArticleResponseDTO>> CreateArticle([FromBody] CreateArticleRequestDTO createArticleRequestDTO)
        {
            var article = ToArticleModel(createArticleRequestDTO);
            var createdArticle = await _articleService.CreateArticleAsync(article);
            return Ok(ToArticleResponseDTO(createdArticle));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleResponseDTO>>> GetAllArticles()
        {
            var articles = await _articleService.GetAllArticles();
            var articleDTOs = articles.Select(ToArticleResponseDTO).ToList();
            return Ok(articleDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ArticleResponseDTO>> GetArticleById(int id)
        {
            var article = await _articleService.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(ToArticleResponseDTO(article));
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var result = await _articleService.DeleteArticle(id);
            if (result == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
