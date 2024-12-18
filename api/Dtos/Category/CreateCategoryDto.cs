using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Category
{
    public class CreateCategoryDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Description must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Description cannot be over 255 characters")]
        public string? Description { get; set; } = string.Empty;
        [Required]
        public bool IsLeaf { get; set; }
    }
}