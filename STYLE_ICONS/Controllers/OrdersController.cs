using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Repository;


namespace WebApplication1.Controllers
{


	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : Controller
	{
		
		private readonly IRepository2 repository;

		public OrdersController(IRepository2 repository)
		{
			this.repository = repository;
		}
		//[HttpGet]
		//public IEnumerable<Order> GetAll()
		//{
		//	return repository.Orders.GetAll();
		//}

		[HttpGet]
		public IActionResult GetOrders( string fk)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var order = repository.Orders.GetByFk(fk);
			

			if (order == null)
			{
				return NotFound();
			}
			return Ok(order);
		}
		[HttpPost]
		public IActionResult Create([FromBody] Order order)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			repository.Orders.Create(order);
			repository.Save();
			return CreatedAtAction("GetOrder", new { id = order.Idorder }, order);
		}
		[HttpPut("{id}")]
		public IActionResult Update([FromRoute] int id, [FromBody] Order order)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var item = repository.Orders.GetByID(id);
			if (item == null)
			{
				return NotFound();
			}
			item.Fkuser = order.Fkuser;
			item.Fkstatus = order.Fkstatus;
			item.Address = order.Address;
			item.Cost = order.Cost;
			repository.Orders.Update(item);
			repository.Save();
			return NoContent();

		}
		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var item = repository.Orders.GetByID(id);
			if (item == null)
			{
				return NotFound();
			}
			repository.Orders.Delete(id);
			repository.Save();
			return NoContent();
		}
	}
}