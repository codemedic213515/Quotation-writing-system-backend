using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public Models.Category? Parent { get; set; }
        public string? Description { get; set; } = string.Empty;    
        public bool IsLeaf { get; set; }
    }
}