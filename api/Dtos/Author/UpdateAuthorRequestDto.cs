using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Author
{
    public class UpdateAuthorRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Author Name must be 5 characters")]
        [MaxLength(255, ErrorMessage = "Author Name cannot be over 255 characters")]
        public string Name { get; set; } = string.Empty;            
    }
}