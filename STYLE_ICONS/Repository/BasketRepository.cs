using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
	public class BasketRepository : IRepository<Basket>
	{
		private ClothingStoreDBContext context;

		public BasketRepository(ClothingStoreDBContext context)
		{
			this.context = context;
		}
		public void Delete(int basketID)
		{
			Basket basket = context.Basket.Find(basketID);
			context.Basket.Remove(basket);
		}
		public List<Basket> GetByFk(string fk)
		{
		
			List<Basket> basket = null;

			try
			{
				basket = context.Basket.Where(b => b.Fkorder == Convert.ToInt32(fk)).ToList();
				

				return basket;
			}
			catch
			{
				return basket;
			}


		}
		public Basket GetByID(int basketID)
		{
			return context.Basket.Find(basketID);
		}

		public List<Basket> GetAll()
		{
			return context.Basket.ToList();
		}

		public void Create(Basket basket)
		{
			List<Basket> baskets = null;
			Order order = null;
			int price = basket.Price;
			Product product = null;
			product = context.Product.Find(basket.Fkproduct);
			try
			{
				baskets = context.Basket.Where(b => b.Fkorder == basket.Fkorder).ToList();

				if (baskets != null)
				{
					baskets = baskets.Where(b => b.Fkproduct == basket.Fkproduct).ToList();
					if (baskets.Count != 0)
					{
						basket.Quantity += baskets[0].Quantity;//количество данного товара
						//if (product.QuantityInStock < baskets[0].Quantity)//если в корзине больше, чем на складе
						//{
						//	return;
						//}
						
						basket.Price = baskets[0].Price * basket.Quantity;//стоимость
						basket.Idbasket = baskets[0].Idbasket;
						context.Entry(basket).State = EntityState.Modified;
					}
					else
					{
						context.Basket.Add(basket);
					}
				}
			}
			catch
			{
				return;
			}

			try
			{
				order = context.Order.Where(b => b.Idorder == basket.Fkorder).FirstOrDefault();//ищем заказ, где содержатся товары

				if (order != null)
				{
					order.Cost += price;
					context.Entry(order).State = EntityState.Modified;
				}
			}
			catch
			{
				return;
			}


		}

		public void Save()
		{
			context.SaveChanges();
		}

		public void Update(Basket basket)
		{
			context.Entry(basket).State = EntityState.Modified;
		}

		
	}
}
