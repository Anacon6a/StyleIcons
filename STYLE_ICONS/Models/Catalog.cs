using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Catalog
    {
        public Catalog()
        {
            Product = new HashSet<Product>();
        }

        public int Idcategory { get; set; }
        public string CatalogName { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
