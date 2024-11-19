using AutoMapper;
using LazyLoadingEagerLoading.Dtos;
using LazyLoadingEagerLoading.Exceptions;
using LazyLoadingEagerLoading.Models;
using LazyLoadingEagerLoading.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LazyLoadingEagerLoading.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IRepository<Book> bookRepository,IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        //lazy load->returns books with their author using lazy loading
        public List<BookDto> GetBooksWithAuthorsLazy()
        {
            //fetch all books from database
            //at this point only book data is loaded and related author data is not yet loaded
            var books = _bookRepository.GetAll().ToList(); //lazy loading occurs when author is accessed

            //author property of each book is not loaded yet
            //EF will not load author until it is specifically accessed
            if (!books.Any())
            {
                throw new BookNotFoundException("No books found");
            }

            //during mapping process(_mapper.Map) when author property is accessed for first time and
            //EF will automatically send separate query to database to load related author for each book
            //and this is where lazy loading happens means loading related data only when it is needed
            return _mapper.Map<List<BookDto>>(books);
        }

        //eager load->returns books with their authors using eager loading
        public List<BookDto> GetBooksWithAuthorsEager()
        {
            //fetching all books along with their related authors in single database query
            //.Include(b => b.Author) tells entity framework to eagerly load related author for each book
            var books = _bookRepository.GetAll().Include(b => b.Author).ToList(); //eager loading ensures that author is loaded with book

            //both books and their associated authors are already loaded from database
            //there are no additional database calls needed later to access author info
            if (!books.Any())
            {
                throw new BookNotFoundException("No books found");
            }
            //list of books with their authors is then mapped to BookDto and returned
            return _mapper.Map<List<BookDto>>(books);
        }
    }
}
