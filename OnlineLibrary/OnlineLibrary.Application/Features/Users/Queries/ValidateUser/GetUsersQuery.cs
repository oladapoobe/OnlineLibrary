using MediatR;
using System;
using System.Collections.Generic;

namespace OnlineLibrary.Application.Features.Users.Queries.ValidateUser
{
    public class GetUsersQuery : IRequest<UsersVm>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public GetUsersQuery(string username, string password)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}
