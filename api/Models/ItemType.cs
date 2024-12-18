using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("ItemType")]
    public class ItemType
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;    
        public string? Description { get; set; } = string.Empty;   
    }
}