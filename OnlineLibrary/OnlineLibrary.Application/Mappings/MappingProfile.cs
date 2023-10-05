using AutoMapper;
using OnlineLibrary.Application.Features.Books.Commands.CheckoutBook;
using OnlineLibrary.Application.Features.Books.Commands.UpdateBook;
using OnlineLibrary.Application.Features.Books.Queries.GetBooksList;
using OnlineLibrary.Application.Features.Users.Commands.CreateUser;
using OnlineLibrary.Application.Features.Users.Queries.ValidateUser;
using OnlineLibrary.Domain.Entities;

namespace Booking.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BooksVm>().ReverseMap();
            CreateMap<Book, CheckoutBookCommand>().ReverseMap();
            CreateMap<Book, UpdateBookCommand>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<UpdateBookCommand, UpdateBookVm>().ReverseMap();
            CreateMap<CheckoutBookCommand, CheckoutBookVm>().ReverseMap();
            CreateMap<User, UsersVm>().ReverseMap();
            CreateMap<CreateUserCommand, CreateUserVm>().ReverseMap();
        }
    }
}
