using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class BookAuthorQueryObject : BookQueryObject
    {
        public string? AuthorName { get; set; } = null;        
    }
}