using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
	public class OrderRepository : IRepository<Order>
	{
		private ClothingStoreDBContext context;

		public OrderRepository(ClothingStoreDBContext context)
		{
			this.context = context;
		}
		public void Delete(int orderID)
		{
			Order order = context.Order.Find(orderID);
			context.Order.Remove(order);
		}
		public List<Order> GetByFk(string userFK)
		{
			List<Order> orders = null;
			try
			{
				orders = context.Order.Where(o => o.Fkuser == userFK).ToList();

				return orders;

			}
			catch
			{
				return orders;
			}


}
			public Order GetByID(int orderID)
		{
			return context.Order.Find(orderID);
		}

		public List<Order> GetAll()
		{
			return context.Order.ToList();
		}

		public void Create(Order order)
		{
			context.Order.Add(order);
	
		}

		public void Save()
		{
			context.SaveChanges();
		}

		public void Update(Order order)
		{
			context.Entry(order).State = EntityState.Modified;
		}

	
	}
}
