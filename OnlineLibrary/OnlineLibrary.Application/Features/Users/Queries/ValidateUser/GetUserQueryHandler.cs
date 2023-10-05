using AutoMapper;
using MediatR;
using OnlineLibrary.Application.Contracts.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;
using OnlineLibrary.Application.Exceptions;

namespace OnlineLibrary.Application.Features.Users.Queries.ValidateUser
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery,UsersVm>
    {
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;
        private readonly IJwtTokenHandler _tokenHandler;

        public GetUsersQueryHandler(IUserRepository UserRepository, IMapper mapper, IJwtTokenHandler tokenHandler)
        {
            _UserRepository = UserRepository ?? throw new ArgumentNullException(nameof(UserRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _tokenHandler = tokenHandler;
        }

        public async Task<UsersVm> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var user = await _UserRepository.GetByUsernameAsync(request.Username);
            // return _mapper.Map<List<UsersVm>>(UserList);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException($"Unauthorized access attempt by user: {request.Username}");

            }
            var token = _tokenHandler.GenerateToken(user);
            var response = _mapper.Map<UsersVm>(user);
            response.token = token;
            return response;
        }
    }
}
