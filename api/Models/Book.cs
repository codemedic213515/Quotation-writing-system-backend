using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Table("Books")]
    public class Book
    {   
        public int ItemId { get; set; }
        [ForeignKey(nameof(ItemId))] 
        public Item Item { get; set; }
        public string ISBN { get; set; }
        public string Subject { get; set; } = string.Empty;
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}