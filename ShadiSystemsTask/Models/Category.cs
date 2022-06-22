using System;
using System.Collections.Generic;

#nullable disable

namespace ShadiSystemsTask.Models
{
    public partial class Category
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }

        public int CatId { get; set; }
        public string CatName { get; set; }
        

        public virtual ICollection<Item> Items { get; set; }
    }
}
