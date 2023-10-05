using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Application.Features.Books.Commands.CheckoutBook;
using OnlineLibrary.Application.Features.Books.Commands.DeleteBook;
using OnlineLibrary.Application.Features.Books.Commands.UpdateBook;
using OnlineLibrary.Application.Features.Books.Queries.GetBooksList;
using OnlineLibrary.Application.Features.Users.Commands.CreateUser;
using System.Net;

namespace OnlineLibrary.API.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BooksController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("GetBooksByCriteria")]
        [ProducesResponseType(typeof(IEnumerable<BooksVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BooksVm>>> GetBooksByCriteria(string? name ="", string? author ="", string? publisher ="")
        {
            var query = new GetBooksListQuery(name, author, publisher);
            var Books = await _mediator.Send(query);
            return Ok(Books);
        }

      
        [HttpPost("AddBook")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> AddBook([FromBody] CheckoutBookVm vm)
        {
            var DateValue = Request.Headers["DateValue"];
            var CreatedBy = Request.Headers["CreatedBy"];
            var command = _mapper.Map<CheckoutBookCommand>(vm);
            command.CreatedBy = CreatedBy;
            command.CreatedDate = DateTime.Parse(DateValue);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdateBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateBook([FromBody] UpdateBookVm vm)
        {
            var DateValue = Request.Headers["DateValue"];
            var CreatedBy = Request.Headers["LastModifiedBy"];
            var command = _mapper.Map<UpdateBookCommand>(vm);
            command.LastModifiedBy = CreatedBy;
            command.LastModifiedDate = DateTime.Parse(DateValue);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("DeleteBook/{id}", Name = "DeleteBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var command = new DeleteBookCommand() { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}