using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Core.Models;
using API.DTOModels;
using static API.Helper.DTOTranslator;

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

        [HttpPost]
        public async Task<ActionResult<ArticleResponseDTO>> CreateArticle([FromBody] CreateArticleRequestDTO createArticleRequestDTO)
        {
            var article = ToArticleModel(createArticleRequestDTO);
            var createdArticle = await _articleService.CreateArticleAsync(article);
            return Ok(ToArticleResponseDTO(createdArticle));
        }
    }
}
