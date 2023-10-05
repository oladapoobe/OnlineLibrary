using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.Features.Books.Commands.CheckoutBook
{
    public class CheckoutBookVm
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
}
