using AutoMapper;
using LazyLoadingEagerLoading.Dtos;
using LazyLoadingEagerLoading.Exceptions;
using LazyLoadingEagerLoading.Models;
using LazyLoadingEagerLoading.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LazyLoadingEagerLoading.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IRepository<Author> authorRepository,IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        
        //lazy load-> it returns authors with books using lazy loading
        public List<AuthorDto> GetAuthorsWithBooksLazy()
        {
            //fetch all authors from database and at this point only author data is loaded
            var authors = _authorRepository.GetAll().ToList(); //lazy loading will occur when books are accessed

            //books property of each author is not loaded yet
            //EF will wait until you specifically access books to fetch it from database
            if (!authors.Any())
            {
                throw new AuthorNotFoundException("No authors found");
            }

            //during mapping process(_mapper.Map) if books property is accessed and
            //EF will make separate queries to database to load related books for each author
            //and this is lazy loading comes here means fetching related data only when needed
            return _mapper.Map<List<AuthorDto>>(authors);
        }

        //eager load-> returns authors with books using eager loading
        public List<AuthorDto> GetAuthorsWithBooksEager()
        {
            //we are fetching all authors along with their related books in single query
            //.Include(a => a.Books) tells entity framework to eagerly load related books for each author
            var authors = _authorRepository.GetAll().Include(a => a.Books).ToList(); //eager loading with include

            //at this point authors and their related books are already loaded from database
            //so we dont need to do any extra queries later for the books
            if (!authors.Any())
            {
                throw new AuthorNotFoundException("No authors found");
            }
            //return authors with their books included as part of result
            return _mapper.Map<List<AuthorDto>>(authors);
        }
    }
}