using MediatR;
using OnlineLibrary.Domain.Common;
using System;

namespace OnlineLibrary.Application.Features.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
