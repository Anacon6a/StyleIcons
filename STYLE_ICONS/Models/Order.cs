using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class Order
    {
        public Order()
        {
            Basket = new HashSet<Basket>();
        }

        public int Idorder { get; set; }
        public string Fkuser { get; set; }
        public int Fkstatus { get; set; }
        public string Address { get; set; }
        public int Cost { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public virtual OrderStatus FkstatusNavigation { get; set; }
        public virtual StoreUser FkuserNavigation { get; set; }
        public virtual ICollection<Basket> Basket { get; set; }
    }
}
