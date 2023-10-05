using MediatR;
using OnlineLibrary.Domain.Common;

namespace OnlineLibrary.Application.Features.Books.Commands.DeleteBook
{
    public class DeleteBookCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
    }
}
