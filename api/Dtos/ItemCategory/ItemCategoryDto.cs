using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.ItemCategory
{
    public class ItemCategoryDto
    {
        public int ItemId { get; set; }
        public int CategoryId { get; set; }    
        public Models.Item Item { get; set; }  
        public Models.Category Category { get; set; }
    }
}