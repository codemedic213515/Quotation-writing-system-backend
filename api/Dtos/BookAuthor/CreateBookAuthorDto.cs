using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.BookAuthor
{
    public class CreateBookAuthorDto
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}