using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; } 
        public int? ParentId { get; set; }   
        public Category? Parent { get; set; }    
        public string? Description { get; set; }
        public bool IsLeaf { get; set; }    
    }
}