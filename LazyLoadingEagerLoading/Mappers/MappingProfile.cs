using AutoMapper;
using LazyLoadingEagerLoading.Dtos;
using LazyLoadingEagerLoading.Models;

namespace LazyLoadingEagerLoading.Mappers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Author,AuthorDto>();
            CreateMap<Book,BookDto>();
        }
    }
}
