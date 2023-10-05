using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Application.Features.Books.Commands.CheckoutBook;
using OnlineLibrary.Application.Features.Users.Commands.CreateUser;
using OnlineLibrary.Application.Features.Users.Queries.ValidateUser;
using OnlineLibrary.Domain.Entities;
using System.Net;

namespace OnlineLibrary.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("Authenticate/{username}/{password}")]
        [ProducesResponseType(typeof(UsersVm), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UsersVm>> GetAuthenticate(string username, string password)
        {
            var query = new GetUsersQuery(username, password);
            var User = await _mediator.Send(query);
            return Ok(User);
        }

        [HttpPost("AddUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> AddUser([FromBody] CreateUserVm vm)
        {
            var DateValue = Request.Headers["DateValue"];
            var CreatedBy = Request.Headers["CreatedBy"];
            var command = _mapper.Map<CreateUserCommand>(vm);
            command.CreatedBy = CreatedBy;
            command.CreatedDate = DateTime.Parse(DateValue);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
