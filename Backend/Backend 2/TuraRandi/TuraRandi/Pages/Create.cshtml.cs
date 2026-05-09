using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TuraRandi.Data;
using TuraRandi.Models;

namespace TuraRandi.Pages
{
    public class CreateModel : PageModel
    {
        private readonly TuraRandi.Data.TuraRandiDbContext _context;

        public CreateModel(TuraRandi.Data.TuraRandiDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Testimonial Testimonial { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Testimonials.Add(Testimonial);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Admin");
        }
    }
}
