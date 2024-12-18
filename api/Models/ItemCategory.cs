using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Table("ItemCategory")]
    public class ItemCategory
    {
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public Item Item { get; set; }
        public Category Category { get; set; }
    }
}