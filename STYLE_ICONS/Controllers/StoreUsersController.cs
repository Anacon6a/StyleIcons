//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using WebApplication1.Models;
//using Microsoft.EntityFrameworkCore;


//namespace WebApplication1.Controllers
//{


//	[Route("api/[controller]")]
//	[ApiController]
//	public class StoreUserController : Controller
//	{
//		private readonly ClothingStoreDBContext _context;
//		public StoreUserController(ClothingStoreDBContext context)
//		{
//			_context = context;
//			//			//if (_context.StoreUser.Count() == 0)
//			//			//{
//			//			//	_context.StoreUser.Add(new StoreUser { Iduser = 1, Fktype = 1, UserLogin = "themad_sam", Password = "dgasj1593", FirstName = "Семён", LastName = "Курицын", MiddleName = "Андреевич", Phone = "89158138822" });
//			//			//	_context.StoreUser.Add(new StoreUser { Iduser = 2, Fktype = 2, UserLogin = "ndorf19", Password = "hgeklgjhtfd", FirstName = "Анастасия", LastName = "Дорофеева", Phone = "89303491122" });
//			//			//	_context.SaveChanges();
//			//			//}
//		}
//		//		[HttpGet]
//		//		public IEnumerable<StoreUser> GetAll()
//		//		{
//		//			return _context.StoreUser.Include(o => o.Order);
//		//		}

//		[HttpGet("{id}")]
//		public async Task<IActionResult> GetStoreUser([FromRoute] string id)
//		{

//			if (!ModelState.IsValid)
//			{
//				return BadRequest(ModelState);
//			}
//			var user = await _context.StoreUser
//			.Where(x => x.Id == id)
//				   .Include("Order")
//				   .ToListAsync();
//			if (user == null)
//			{
//				return NotFound();
//			}
//			return Ok(user);
//		}
//	}
//}
//		[HttpPost]
//		public async Task<IActionResult> Create([FromBody] StoreUser user)
//		{
//			if (!ModelState.IsValid)
//			{
//				return BadRequest(ModelState);
//			}
//			_context.StoreUser.Add(user);
//			await _context.SaveChangesAsync();
//			return CreatedAtAction("GetStoreUser", new { id = user.Id }, user);
//		}
//		[HttpPut("{id}")]
//		public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StoreUser user)
//		{
//			if (!ModelState.IsValid)
//			{
//				return BadRequest(ModelState);
//			}
//			var item = _context.StoreUser.Find(id);
//			if (item == null)
//			{
//				return NotFound();
//			}
//			item.Fktype = user.Fktype;
//			item.UserName = user.UserName;
//			item.PasswordHash = user.PasswordHash;
//			item.FirstName = user.FirstName;
//			item.LastName = user.LastName;
//			item.MiddleName = user.MiddleName;
//			item.Email = user.Email;
//			item.PhoneNumber = user.PhoneNumber;
//			_context.StoreUser.Update(item);
//			await _context.SaveChangesAsync();
//			return NoContent();

//		}
//		[HttpDelete("{id}")]
//		public async Task<IActionResult> Delete([FromRoute] int id)
//		{
//			if (!ModelState.IsValid)
//			{
//				return BadRequest(ModelState);
//			}
//			var item = _context.StoreUser.Find(id);
//			if (item == null)
//			{
//				return NotFound();
//			}
//			_context.StoreUser.Remove(item);
//			await _context.SaveChangesAsync();
//			return NoContent();
//		}
//	}
//}

