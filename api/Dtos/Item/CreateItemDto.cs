using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Item
{
    public class CreateItemDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Item Name must be 5 characters")]
        [MaxLength(255, ErrorMessage = "Item Name cannot be over 255 characters")]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int TypeId { get; set; } 
        [MaxLength(255, ErrorMessage = "Image File cannot be over 255 characters")]
        public string? ImageFileSpec { get; set; }
        [MaxLength(2000, ErrorMessage = "Description cannot be over 2000 characters")]
        public string? Description { get; set; }
        [Required]
        [Range(1.01, 100000)]
        public double? UnitCost { get; set; }   
        [Required]
        [Range(1.01, 100000)]
        public double UnitPrice { get; set; }        
    }
}