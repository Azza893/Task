using System;
using System.Collections.Generic;

#nullable disable

namespace ShadiSystemsTask.Models
{
    public partial class Item
    {
        public Item()
        {
            ItemsStores = new HashSet<ItemsStore>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int CatId { get; set; }
        public double? Price { get; set; }

        public virtual Category Cat { get; set; }
        public virtual ICollection<ItemsStore> ItemsStores { get; set; }
    }
}
