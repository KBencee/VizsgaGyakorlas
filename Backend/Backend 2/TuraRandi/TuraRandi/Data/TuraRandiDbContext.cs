using Microsoft.EntityFrameworkCore;
using TuraRandi.Models;

namespace TuraRandi.Data
{
	public class TuraRandiDbContext : DbContext
	{
		//Hozza létre az osztály konstruktorát, amely átveszi a DbContextOptions paramétert






		// Add DbSet properties here
		public DbSet<Testimonial> Testimonials { get; set; }

	}
}
