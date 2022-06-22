using System;
using System.Collections.Generic;

#nullable disable

namespace ShadiSystemsTask.Models
{
    public partial class ItemsStore
    {
        public long Id { get; set; }
        public int ItemId { get; set; }
        public double? Quantity { get; set; }

        public virtual Item Item { get; set; }
    }
}
