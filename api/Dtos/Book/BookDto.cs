using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using api.Models;

namespace api.Dtos.Book
{
    public class BookDto
    {
        public api.Models.Item Item { get; set; }
        public string ISBN { get; set; }
        public string Subject { get; set; } = string.Empty;
        public api.Models.Publisher Publisher { get; set; }  
    }
}