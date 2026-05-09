using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TuraRandi.Data;
using TuraRandi.Models;

namespace TuraRandi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TuraRandi.Data.TuraRandiDbContext _context;

        public IndexModel(TuraRandi.Data.TuraRandiDbContext context)
        {
            _context = context;
        }

        public IList<Testimonial> Testimonial { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Testimonial = await _context.Testimonials.ToListAsync();
        }
    }
}
