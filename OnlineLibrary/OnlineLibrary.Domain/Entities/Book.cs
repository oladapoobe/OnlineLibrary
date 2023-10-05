using OnlineLibrary.Domain.Common;

namespace OnlineLibrary.Domain.Entities
{
    public class Book : EntityBase
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }
}


