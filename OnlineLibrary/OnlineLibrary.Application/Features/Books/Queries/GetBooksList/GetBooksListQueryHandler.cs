using AutoMapper;
using MediatR;
using OnlineLibrary.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.Features.Books.Queries.GetBooksList
{
    public class GetBooksListQueryHandler : IRequestHandler<GetBooksListQuery, List<BooksVm>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBooksListQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<BooksVm>> Handle(GetBooksListQuery request, CancellationToken cancellationToken)
        {
            var BookList = await _bookRepository.GetBooksByCriteria(request.Author,request.Publisher, request.Name);
            return _mapper.Map<List<BooksVm>>(BookList);
        }
    }
}
