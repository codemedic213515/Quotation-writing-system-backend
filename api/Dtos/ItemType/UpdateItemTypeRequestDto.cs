using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ItemType
{
    public class UpdateItemTypeRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Item Type Name must be 5 characters")]
        [MaxLength(255, ErrorMessage = "Item Type Name cannot be over 255 characters")]
        public string Name { get; set; } = string.Empty;   

        [MinLength(5, ErrorMessage = "Item Type Description must be 5 characters")]
        [MaxLength(2000, ErrorMessage = "Item Type Description cannot be over 2000 characters")]
        public string Description { get; set; } = string.Empty;           
    }
}