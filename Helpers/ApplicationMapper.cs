using AutoMapper;
using BookStore.Data;
using BookStore.Models;

namespace BookStore.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            // CreateMap<Source, Destination>();
            // Example: CreateMap<BooksModel, BooksDTO>();
            CreateMap<Books, BooksModel>().ReverseMap();


        }
    }
}
