using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineLibrary.Application.Contracts.Persistence;
using OnlineLibrary.Application.Exceptions;
using OnlineLibrary.Application.Features.Users.Queries.ValidateUser;
using OnlineLibrary.Domain.Common;
using OnlineLibrary.Domain.Entities;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseDto>
    {
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRepository UserRepository, IMapper mapper, ILogger<CreateUserCommandHandler> logger)
        {
            _UserRepository = UserRepository ?? throw new ArgumentNullException(nameof(UserRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var UserRecord = await _UserRepository.GetByUsernameAsync(request.Username);
            if (UserRecord != null)
                return new ResponseDto { Success = false, Message= $"Username {request.Username} is already taken" };  
                 
 
            var UserEntity = _mapper.Map<User>(request);
            UserEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var newUser = await _UserRepository.AddAsync(UserEntity);
            
            _logger.LogInformation($"User {request.Username} is successfully created.");

            return new ResponseDto { Success = true, Message = $"User {request.Username} is successfully created." };
           
        }

    }
}
