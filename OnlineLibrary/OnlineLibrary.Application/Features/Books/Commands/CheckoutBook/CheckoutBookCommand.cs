using MediatR;
using OnlineLibrary.Domain.Common;
using System;

namespace OnlineLibrary.Application.Features.Books.Commands.CheckoutBook
{
    public class CheckoutBookCommand : IRequest<ResponseDto>
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
