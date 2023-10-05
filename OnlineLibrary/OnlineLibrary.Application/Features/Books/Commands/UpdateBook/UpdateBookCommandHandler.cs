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

namespace OnlineLibrary.Application.Features.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, ResponseDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBookCommandHandler> _logger;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper, ILogger<UpdateBookCommandHandler> logger)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ResponseDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var BookToUpdate = await _bookRepository.GetByIdAsync(request.Id);
            if (BookToUpdate == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }
            
            _mapper.Map(request, BookToUpdate, typeof(UpdateBookCommand), typeof(Book));

            await _bookRepository.UpdateAsync(BookToUpdate);

            _logger.LogInformation($"Book {BookToUpdate.Id} is successfully updated.");

            return new ResponseDto { Success = true, Message = $"Book {BookToUpdate.Id} is successfully updated." };

          
        }
    }
}
