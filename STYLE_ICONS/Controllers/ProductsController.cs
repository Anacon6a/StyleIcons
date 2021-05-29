    using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc;
	using WebApplication1.Models;
	using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace WebApplication1.Controllers
{


	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : Controller
	{
		private readonly ClothingStoreDBContext _context;
		public ProductsController(ClothingStoreDBContext context)
		{
			_context = context;
			//if (_context.Product.Count() == 0)
			//{
			//	_context.Product.Add(new Product { Idproduct = 1, Fkcategory = 1, ProductName = "продукт1", Price = 1111, QuantityInStock = 12 });
			//	_context.Product.Add(new Product { Idproduct = 2, Fkcategory = 2, ProductName = "продукт2", Price = 2222, QuantityInStock = 13 });
			//	_context.SaveChanges();
			//}
		}
		[HttpGet]
		public IEnumerable<Product> GetAll()
		{
			return _context.Product.Include(p => p.Basket);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProduct([FromRoute] int id)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var product = await _context.Product.SingleOrDefaultAsync(x => x.Idproduct == id);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}

		[Authorize(Roles = "admin")]
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Product product)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_context.Product.Add(product);
			await _context.SaveChangesAsync();
			return CreatedAtAction("GetProduct", new { id = product.Idproduct }, product);
		}
		[Authorize(Roles = "admin")]
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Product product)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var item = _context.Product.Find(id);
			if (item == null)
			{
				return NotFound();
			}
			item.Fkcategory = product.Fkcategory;
			item.ProductName = product.ProductName;
			item.ProductImage = product.ProductImage;
			item.Price = product.Price;
			item.QuantityInStock = product.QuantityInStock;

			_context.Product.Update(item);
			await _context.SaveChangesAsync();
			return NoContent();

		}
		[Authorize(Roles = "admin")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var item = _context.Product.Find(id);
			if (item == null)
			{
				return NotFound();
			}
			_context.Product.Remove(item);
			await _context.SaveChangesAsync();
			return NoContent();
		}
		//[Authorize(Roles = "admin")]
		//[HttpDelete("{fk}")]
		//public async Task<IActionResult> DeleteFromCatalog([FromRoute] int id)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}
		//	var item = await _context.Product
		//		.Where(x => x.Fkcategory == id)
		//		.SingleOrDefaultAsync();

		//	if (item == null)
		//	{
		//		return NotFound();
		//	}
		//	_context.Product.Remove(item);
		//	await _context.SaveChangesAsync();
		//	return NoContent();
		//}
	}
}

