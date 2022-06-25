using ShadiSystemsTask.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShadiSystemsTask.ViewModels
{
    public class ProductViewModel
    {
       
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
       
        public int CatId { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }

        public int ItemStoreId { get; set; }
        [ForeignKey("CatId")]
        public virtual Category Cat { get; set; }
        [ForeignKey("Id")]
        public virtual ICollection<ItemsStore> ItemsStores { get; set; }
    }
}
