using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
	public class DbInitializer
	{
		public static void Initialize(ClothingStoreDBContext context)
		{
			context.Database.EnsureCreated();
			//if (!context.TypeUser.Any())
			//{
			//	var typeuser = new TypeUser[]
			//	{
			//	new TypeUser {TypeUser1 = "Администратор"},
			//	new TypeUser {TypeUser1 = "Покупатель"}
			//	};
			//	foreach (TypeUser tu in typeuser)
			//	{
			//		context.TypeUser.Add(tu);
			//	}
			//	context.SaveChanges();
			//}
			//if (!context.StoreUser.Any())
			//{
			//	var user = new StoreUser[]
			//	{
			//	new StoreUser{Fktype = 1, UserName = "themad_sam", PasswordHash = "dgasj1593", FirstName = "Семён", LastName = "Курицын", MiddleName = "Андреевич", PhoneNumber = "89158138822"},
			//	new StoreUser{Fktype = 2, UserName = "ndorf19", PasswordHash = "hgeklgjhtfd", FirstName = "Анастасия", LastName = "Дорофеева", PhoneNumber = "89303491122"},
			//	new StoreUser{Fktype = 2, UserName = "helen.pp", PasswordHash = "andrlex", FirstName = "Елена",  LastName = "Парахина",  PhoneNumber = "89631513322"}
			//	};
			//	foreach (StoreUser u in user)
			//	{
			//		context.StoreUser.Add(u);
			//	}
			//	context.SaveChanges();
			//}
			if (!context.Catalog.Any())
			{

				var catalog = new Catalog[]
				{
				new Catalog{CatalogName = "Платья"},
				new Catalog{CatalogName = "Куртки и пальто"},
				new Catalog{CatalogName = "Свитшоты и худи"},
				new Catalog{CatalogName = "Рубашки и блузки"},
				new Catalog{CatalogName = "Юбки"},
				new Catalog{CatalogName = "Футболки и топы"},
				new Catalog{CatalogName = "Брюки и джинсы"}
				};
				foreach (Catalog с in catalog)
				{
					context.Catalog.Add(с);
				}
				context.SaveChanges();
			}

			if (!context.Product.Any())
			{
				var product = new Product[]
				{
				new Product{Fkcategory = 1, ProductName="Платье в стиле поло", Price=2600, QuantityInStock=65},
				new Product{Fkcategory = 5, ProductName="Юбка из искусственной кожи", Price=1999, QuantityInStock=35},
				new Product{Fkcategory = 6, ProductName="Топ с открытыми плечами", Price=1599, QuantityInStock=43},
				new Product{Fkcategory = 1, ProductName="Платье асимметрического кроя с волнами", Price=4799, QuantityInStock=34},
				new Product{Fkcategory = 2, ProductName="Укороченная джинсовая куртка", Price=2999, QuantityInStock=61},
				new Product{Fkcategory = 7, ProductName="Брюки джогеры с вышивкой", Price=1999, QuantityInStock=49},
				new Product{Fkcategory = 4, ProductName="Блуза с бантами", Price=1799, QuantityInStock=59}
				};
				foreach (Product p in product)
				{
					context.Product.Add(p);
				}
				context.SaveChanges();
			}
			if (!context.OrderStatus.Any())
			{
				var status = new OrderStatus[]
				{
				new OrderStatus{StatusName = "Не сформирован"},
				new OrderStatus{StatusName = "В пути"},
				new OrderStatus{StatusName = "Доставлено"},
				};
				foreach (OrderStatus s in status)
				{
					context.OrderStatus.Add(s);
				}
				context.SaveChanges();
			}
			//if (!context.Order.Any())
			//{
			//	var order = new Order[]
			//	{
			//	new Order{Fkuser = 19, Fkstatus = 1, Address = "г.Москва, ул.Стандартная, д. 21, кв.35"  },
			//	new Order{Fkuser = 20,  Fkstatus = 1, Address = "г.Иваново, ул.Кавалерийская, д. 5, кв.115" },
			//	};
			//	foreach (Order o in order)
			//	{
			//		context.Order.Add(o);
			//	}
			//	context.SaveChanges();
			//}
			//if (!context.Order.Any())
			//{
			//	var basket = new Basket[]
			//	{
			//	new Basket{Idbasket = 1, Fkorder = 1, Fkproduct = 2, Quantity = 2, Price = 2999},
			//	new Basket{Idbasket = 2, Fkorder = 1, Fkproduct = 5, Quantity = 2, Price = 1999},
			//	new Basket{Idbasket = 3, Fkorder = 2, Fkproduct = 4, Quantity = 2, Price = 1599},
			//	};
			//	foreach (Basket o in basket)
			//	{
			//		context.Basket.Add(o);
			//	}
			//	context.SaveChanges();
			//}
		}
	}
	
}
