using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ItemCategory
{
    public class CreateItemCategoryDto
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int CategoryId { get; set; }    
    }
}