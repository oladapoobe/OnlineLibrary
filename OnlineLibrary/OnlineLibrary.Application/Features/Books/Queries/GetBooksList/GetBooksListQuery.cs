using MediatR;
using System;
using System.Collections.Generic;

namespace OnlineLibrary.Application.Features.Books.Queries.GetBooksList
{
    public class GetBooksListQuery : IRequest<List<BooksVm>>
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }

        public GetBooksListQuery(string name, string author, string publisher)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }
    }
}
