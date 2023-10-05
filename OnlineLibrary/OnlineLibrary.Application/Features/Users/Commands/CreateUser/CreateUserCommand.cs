using MediatR;
using OnlineLibrary.Domain.Common;
using System;

namespace OnlineLibrary.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<ResponseDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
