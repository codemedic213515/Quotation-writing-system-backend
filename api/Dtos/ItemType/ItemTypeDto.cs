using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ItemType
{
    public class ItemTypeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;    
        public string? Description { get; set; } = string.Empty;       
    }
}