using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
	public interface IRepository2 : IDisposable
	{
		IRepository<Basket> Baskets { get; }

		IRepository<Order> Orders { get; }
		//IRepository<OrderStatus> OrderStatuses { get; }


		int Save();
	}
}
