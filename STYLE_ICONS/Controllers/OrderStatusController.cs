using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApplication1.Controllers
{


	[Route("api/[controller]")]
	[ApiController]
	public class OrderStatusController : Controller
	{
		private readonly ClothingStoreDBContext _context;
		public OrderStatusController(ClothingStoreDBContext context)
		{
			_context = context;
			//if (_context.OrderStatus.Count() == 0)
			//{
			//	_context.OrderStatus.Add(new OrderStatus { Idstatus = 1, StatusName = "Статус1" });
			//	_context.OrderStatus.Add(new OrderStatus { Idstatus = 2, StatusName = "Статус2" });
			//	_context.SaveChanges();
			//}
		}
		[HttpGet]
		public IEnumerable<OrderStatus> GetAll()
		{
			return _context.OrderStatus.Include(o => o.Order);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderStatus([FromRoute] int id)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var status = await _context.OrderStatus.SingleOrDefaultAsync(x => x.Idstatus == id);
			if (status == null)
			{
				return NotFound();
			}
			return Ok(status);
		}
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] OrderStatus status)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_context.OrderStatus.Add(status);
			await _context.SaveChangesAsync();
			return CreatedAtAction("GetOrderStatus", new { id = status.Idstatus }, status);
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] OrderStatus status)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var item = _context.OrderStatus.Find(id);
			if (item == null)
			{
				return NotFound();
			}
			
			item.StatusName = status.StatusName;
			_context.OrderStatus.Update(item);
			await _context.SaveChangesAsync();
			return NoContent();

		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var item = _context.OrderStatus.Find(id);
			if (item == null)
			{
				return NotFound();
			}
			_context.OrderStatus.Remove(item);
			await _context.SaveChangesAsync();
			return NoContent();
		}
	}
}

