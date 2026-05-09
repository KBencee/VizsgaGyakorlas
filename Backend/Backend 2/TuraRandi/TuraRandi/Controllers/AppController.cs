using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuraRandi.Data;
using TuraRandi.Models;

namespace TuraRandi.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AppController : ControllerBase
	{
		//Adatbázis kapcsolat példányosítása _context néven





		// GET: app
		//Bejegyzések listájának lekérése
		[HttpGet]
		public async Task<ActionResult<List<Testimonial>>> GetTestimonialList()
		{
			return _context.Testimonials.ToList();
		}


		// GET: app/5
		//Bejegyzés lekérése
		







		// POST: app
		//Bejegyzés hozzáadása
		[HttpPost]
		public async Task<ActionResult> PostTestimonial()
		{
			
			return Ok();
		}

		// PUT: app/5
		//Bejegyzés módosítása
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTestimonial(int id, Testimonial testimonial)
		{
			if (id != testimonial.Id)
			{
				return BadRequest();
			}
			_context.Entry(testimonial).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// DELETE: app/5
		//Bejegyzés törlése
		






	}
}
