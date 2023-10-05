using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineLibrary.Application.Contracts.Persistence;
using OnlineLibrary.Domain.Common;
using OnlineLibrary.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.Features.Books.Commands.CheckoutBook
{
    public class CheckoutBookCommandHandler : IRequestHandler<CheckoutBookCommand, ResponseDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutBookCommandHandler> _logger;

        public CheckoutBookCommandHandler(IBookRepository bookRepository, IMapper mapper, ILogger<CheckoutBookCommandHandler> logger)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ResponseDto> Handle(CheckoutBookCommand request, CancellationToken cancellationToken)
        {
            var UserRecord = await _bookRepository.GetAsync(x=>x.Name == request.Name);
            if (UserRecord.Count > 0)
                return new ResponseDto { Success = false, Message = $"Book Name {request.Name} is already taken" };


            var BookEntity = _mapper.Map<Book>(request);
            var newBook = await _bookRepository.AddAsync(BookEntity);
            
            _logger.LogInformation($"Book {newBook.Name} is successfully created.");

            return new ResponseDto { Success = true, Message = $"Book {newBook.Name} is successfully created." };

        }

    }
}
