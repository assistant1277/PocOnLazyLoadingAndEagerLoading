using LazyLoadingEagerLoading.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LazyLoadingEagerLoading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("lazyloading")]
        public IActionResult GetAuthorsWithBooksLazy()
        {
            var authors = _authorService.GetAuthorsWithBooksLazy();
            return Ok(authors);
        }

        [HttpGet("eagerloading")]
        public IActionResult GetAuthorsWithBooksEager()
        {
            var authors = _authorService.GetAuthorsWithBooksEager();
            return Ok(authors);
        }
    }
}