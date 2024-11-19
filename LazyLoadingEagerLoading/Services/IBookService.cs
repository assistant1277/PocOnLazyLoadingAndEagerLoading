using LazyLoadingEagerLoading.Dtos;

namespace LazyLoadingEagerLoading.Services
{
    public interface IBookService
    {
        public List<BookDto> GetBooksWithAuthorsLazy();
        public List<BookDto> GetBooksWithAuthorsEager();
    }
}
