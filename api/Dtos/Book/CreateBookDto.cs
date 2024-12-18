using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Book
{
    public class CreateBookDto
    {
        [Required]
        public int ItemId { get; set; }   

        [MinLength(10, ErrorMessage = "Subject must be 10 characters")]
        [MaxLength(2000, ErrorMessage = "Subject cannot be over 2000 characters")]
        public string? Subject { get; set; } = string.Empty;

        [Required]
        [MinLength(10, ErrorMessage = "ISBN must be 10 characters")]
        [MaxLength(20, ErrorMessage = "ISBN cannot be over 20 characters")]
        public string ISBN { get; set; } = string.Empty;        
        
        [Required]
        public int PublisherId { get; set; }       
    }
}