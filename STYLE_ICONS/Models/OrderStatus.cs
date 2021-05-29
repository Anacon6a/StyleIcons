using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Order = new HashSet<Order>();
        }

        public int Idstatus { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
