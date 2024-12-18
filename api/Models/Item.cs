using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Table("Items")]
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TypeId { get; set; } 
        public ItemType Type { get; set; }
        public string? ImageFileSpec { get; set; }
        public string? Description { get; set; }
        public double? UnitCost { get; set; }   
        public double UnitPrice { get; set; }
    }
}