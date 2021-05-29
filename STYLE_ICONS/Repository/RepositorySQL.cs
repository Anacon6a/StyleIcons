using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
	public class RepositorySQL : IRepository2
	{
		private ClothingStoreDBContext context;
		private BasketRepository basketRepository;
		private OrderRepository orderRepository;

		public RepositorySQL(ClothingStoreDBContext context)
		{
			this.context = context;
		}
		public IRepository<Basket> Baskets
		{
			get
			{
				if (basketRepository == null)
					basketRepository = new BasketRepository(context);
				return basketRepository;
			}
		}
		public IRepository<Order> Orders
		{
			get
			{
				if (orderRepository == null)
					orderRepository = new OrderRepository(context);
				return orderRepository;
			}
		}


		private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
