using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Basket
    {
        public int Idbasket { get; set; }
        public int Fkorder { get; set; }
        public int Fkproduct { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public virtual Order FkorderNavigation { get; set; }
        public virtual Product FkproductNavigation { get; set; }
    }
}
