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
	public class BasketsController : Controller
	{
		private readonly IRepository2 repository;

		public BasketsController(IRepository2 repository)
		{
			this.repository = repository;
		}

		//[HttpGet]
		//public IEnumerable<Basket> GetAll()
		//{
		//	return repository.Baskets.GetAll();
		//}

		[HttpGet]
		public IActionResult GetBasket(int fk)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var basket = repository.Baskets.GetByFk(Convert.ToString(fk));

			if (basket == null)
			{
				return NotFound();
			}
			return Ok(basket);

		}
		[HttpPost]
		public IActionResult Create([FromBody] Basket basket)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
		    repository.Baskets.Create(basket);

			repository.Save();
			return CreatedAtAction("GetBasket", new { id = basket.Idbasket }, basket);
		}
		[HttpPut("{id}")]
		public IActionResult Update([FromRoute] int id, [FromBody] Basket basket)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var item = repository.Baskets.GetByID(id);
			if (item == null)
			{
				return NotFound();
			}
			item.Fkorder = basket.Fkorder;
			item.Fkproduct = basket.Fkproduct;
			item.Quantity = basket.Quantity;
			item.Price = basket.Price;
			repository.Baskets.Update(item);
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
			var item = repository.Baskets.GetByID(id);
			if (item == null)
			{
				return NotFound();
			}
			repository.Baskets.Delete(id);
			repository.Save();
			return NoContent();
		}
	}
}

