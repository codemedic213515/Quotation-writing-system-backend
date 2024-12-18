using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Table("Publishers")]
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
     }
}