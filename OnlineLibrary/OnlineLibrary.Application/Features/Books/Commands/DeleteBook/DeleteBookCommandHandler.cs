using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineLibrary.Application.Contracts.Persistence;
using OnlineLibrary.Application.Exceptions;
using OnlineLibrary.Domain.Common;
using OnlineLibrary.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.Features.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, ResponseDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteBookCommandHandler> _logger;

        public DeleteBookCommandHandler(IBookRepository bookRepository, IMapper mapper, ILogger<DeleteBookCommandHandler> logger)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ResponseDto> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var BookToDelete = await _bookRepository.GetByIdAsync(request.Id);
            if (BookToDelete == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }            

            await _bookRepository.DeleteAsync(BookToDelete);

            _logger.LogInformation($"Book {BookToDelete.Id} is successfully deleted.");

            return new ResponseDto { Success = true, Message = $"Book {BookToDelete.Id} is successfully deleted." };
        }
    }
}
