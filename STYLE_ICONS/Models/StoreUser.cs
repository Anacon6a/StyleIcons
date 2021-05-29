using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public partial class StoreUser: IdentityUser
    {
        public StoreUser()
        {
            Order = new HashSet<Order>();
        }

      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
