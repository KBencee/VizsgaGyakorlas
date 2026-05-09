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
    public class DetailsModel : PageModel
    {
        private readonly TuraRandi.Data.TuraRandiDbContext _context;

        public DetailsModel(TuraRandi.Data.TuraRandiDbContext context)
        {
            _context = context;
        }

        public Testimonial Testimonial { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials.FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }
            else
            {
                Testimonial = testimonial;
            }
            return Page();
        }
    }
}
