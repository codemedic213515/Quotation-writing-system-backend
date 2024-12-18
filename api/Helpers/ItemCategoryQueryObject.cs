using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class ItemCategoryQueryObject : ItemQueryObject
    {
         public string? CategoryDescription { get; set; } = null;       
    }
}