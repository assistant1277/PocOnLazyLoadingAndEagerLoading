using LazyLoadingEagerLoading.Dtos;

namespace LazyLoadingEagerLoading.Services
{
    public interface IAuthorService
    {
        public List<AuthorDto> GetAuthorsWithBooksLazy();
        public List<AuthorDto> GetAuthorsWithBooksEager();
    }
}
