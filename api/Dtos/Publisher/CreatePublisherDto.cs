using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Publisher
{
    public class CreatePublisherDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Publisher Name must be 5 characters")]
        [MaxLength(255, ErrorMessage = "Publisher Name cannot be over 255 characters")]
        public string Name { get; set; } = string.Empty;     
    }
}