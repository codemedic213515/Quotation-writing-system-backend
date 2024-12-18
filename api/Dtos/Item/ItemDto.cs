using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Item
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //public int TypeId { get; set; } 
        public Models.ItemType Type { get; set; }
        public string? ImageFileSpec { get; set; }
        public string? Description { get; set; }
        public double? UnitCost { get; set; }   
        public double UnitPrice { get; set; }      
    }
}