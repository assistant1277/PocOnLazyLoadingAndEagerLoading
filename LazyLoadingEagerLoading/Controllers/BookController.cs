using LazyLoadingEagerLoading.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LazyLoadingEagerLoading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("lazyloading")]
        public IActionResult GetBooksWithAuthorsLazy()
        {
            var books = _bookService.GetBooksWithAuthorsLazy();
            return Ok(books);
        }

        [HttpGet("eagerloading")]
        public IActionResult GetBooksWithAuthorsEager()
        {
            var books = _bookService.GetBooksWithAuthorsEager();
            return Ok(books);
        }
    }
}