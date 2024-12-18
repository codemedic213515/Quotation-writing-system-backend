using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class BookQueryObject
    {
        public string? ISBN { get; set; } = null;
        public string? Name { get; set; } = null;    
        public string? Subject { get; set; } = null;
        public string? Type { get; set; } = null;
        public string? PublisherName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;          
    }
}