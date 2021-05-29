using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Product
    {
        public Product()
        {
            Basket = new HashSet<Basket>();
        }

        public int Idproduct { get; set; }
        public int Fkcategory { get; set; }
        public string ProductName { get; set; }

        public string ProductImage { get; set; }
        public int Price { get; set; }
        public int QuantityInStock { get; set; }

        public virtual Catalog FkcategoryNavigation { get; set; }
        public virtual ICollection<Basket> Basket { get; set; }
    }
}
