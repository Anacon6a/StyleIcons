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
	public class CatalogsController : Controller
	{
		private readonly ClothingStoreDBContext _context;
		public CatalogsController(ClothingStoreDBContext context)
		{
			_context = context;
			if (_context.Catalog.Count() == 0)
			{
				_context.Catalog.Add(new Catalog { Idcategory = 1, CatalogName = "Каталог1" });
				_context.Catalog.Add(new Catalog { Idcategory = 2, CatalogName = "Каталог2" });
				_context.SaveChanges();
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var catalogs = await _context.Catalog.ToListAsync();
			return Ok(catalogs);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCatalog([FromRoute] int id)
		{

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
		
			var catalog = await _context.Catalog
				.Where(x => x.Idcategory == id)
					   .Include("Product")
					   .ToListAsync();

			if (catalog == null)
			{
				return NotFound();
			}

			return Ok(catalog);
		}
		[Authorize(Roles = "admin")]
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Catalog catalog)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_context.Catalog.Add(catalog);
			await _context.SaveChangesAsync();
			var g = CreatedAtAction("GetCatalog", new { id = catalog.Idcategory }, catalog); 
			return CreatedAtAction("GetCatalog", new { id = catalog.Idcategory }, catalog);
		}
		[Authorize(Roles = "admin")]
		[HttpPut("{id}")]
		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Catalog catalog)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var item = _context.Catalog.Find(id);
			if (item == null)
			{
				return NotFound();
			}
		
			item.CatalogName = catalog.CatalogName;
			_context.Catalog.Update(item);
			await _context.SaveChangesAsync();
			return NoContent();

		}
		[Authorize(Roles = "admin")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)//удаление каталога и связанных с ним товаров
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var catalog = await _context.Catalog //находим каталог
			.Where(x => x.Idcategory == id)
			.FirstOrDefaultAsync();
			if (catalog == null)
			{
				return NotFound();
			}
			var products = await _context.Product //находим товары в каталоге
			.Where(x => x.Fkcategory == id)
			.ToListAsync();
			if (products != null)
			{
				_context.Product.RemoveRange(products);
			}

			_context.Catalog.Remove(catalog);
			await _context.SaveChangesAsync();
			return NoContent();

		}
	}
}

