using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArabaGaleri.Data;
using ArabaGaleri.Models;

namespace ArabaGaleri.Controllers
{
    public class ArabaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArabaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string markaFilter)
        {
            var arabalar = from a in _context.Arabalar
                          select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                arabalar = arabalar.Where(a => a.Marka.Contains(searchString) 
                    || a.Model.Contains(searchString) 
                    || (a.Aciklama != null && a.Aciklama.Contains(searchString)));
            }

            if (!string.IsNullOrEmpty(markaFilter))
            {
                arabalar = arabalar.Where(a => a.Marka == markaFilter);
            }

            arabalar = arabalar.Where(a => !a.SatildiMi).OrderByDescending(a => a.OlusturmaTarihi);

            var markalar = await _context.Arabalar
                .Where(a => !a.SatildiMi)
                .Select(a => a.Marka)
                .Distinct()
                .OrderBy(m => m)
                .ToListAsync();

            ViewBag.Markalar = markalar;
            ViewBag.SearchString = searchString;
            ViewBag.MarkaFilter = markaFilter;

            return View(await arabalar.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var araba = await _context.Arabalar
                .FirstOrDefaultAsync(m => m.Id == id);

            if (araba == null)
            {
                return NotFound();
            }

            return View(araba);
        }
    }
}




