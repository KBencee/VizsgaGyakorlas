using Microsoft.EntityFrameworkCore;
using TuraRandi.Models;

namespace TuraRandi.Data
{
	public class TuraRandiDbContext : DbContext
	{
		public TuraRandiDbContext(DbContextOptions<TuraRandiDbContext> options) : base(options)
		{

		}

		// Itt add hozzá a DbSet tulajdonságot a modellekhez Testimonials néven

	}
}
