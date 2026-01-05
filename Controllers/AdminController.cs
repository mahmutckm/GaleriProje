using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArabaGaleri.Data;
using ArabaGaleri.Models;

namespace ArabaGaleri.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var arabalarCount = await _context.Arabalar.CountAsync();
            var satilmisCount = await _context.Arabalar.CountAsync(a => a.SatildiMi);
            var satilmamisCount = await _context.Arabalar.CountAsync(a => !a.SatildiMi);
            var kullanicilarCount = await _context.Kullanicilar.CountAsync();

            ViewBag.ArabalarCount = arabalarCount;
            ViewBag.SatilmisCount = satilmisCount;
            ViewBag.SatilmamisCount = satilmamisCount;
            ViewBag.KullanicilarCount = kullanicilarCount;

            return View();
        }

        public async Task<IActionResult> Arabalar()
        {
            var arabalar = await _context.Arabalar.OrderByDescending(a => a.OlusturmaTarihi).ToListAsync();
            return View(arabalar);
        }

        public IActionResult ArabaCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArabaCreate([Bind("Marka,Model,Yil,Fiyat,Kilometre,YakıtTipi,VitesTipi,Renk,Aciklama,ResimUrl,SatildiMi")] Araba araba)
        {
            if (ModelState.IsValid)
            {
                _context.Add(araba);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Araba başarıyla eklendi.";
                return RedirectToAction(nameof(Arabalar));
            }
            return View(araba);
        }

        public async Task<IActionResult> ArabaEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var araba = await _context.Arabalar.FindAsync(id);
            if (araba == null)
            {
                return NotFound();
            }
            return View(araba);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArabaEdit(int id, [Bind("Id,Marka,Model,Yil,Fiyat,Kilometre,YakıtTipi,VitesTipi,Renk,Aciklama,ResimUrl,SatildiMi,OlusturmaTarihi")] Araba araba)
        {
            if (id != araba.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(araba);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Araba başarıyla güncellendi.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArabaExists(araba.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Arabalar));
            }
            return View(araba);
        }

        public async Task<IActionResult> ArabaDelete(int? id)
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

        [HttpPost, ActionName("ArabaDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ArabaDeleteConfirmed(int id)
        {
            var araba = await _context.Arabalar.FindAsync(id);
            if (araba != null)
            {
                _context.Arabalar.Remove(araba);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Araba başarıyla silindi.";
            }

            return RedirectToAction(nameof(Arabalar));
        }

        private bool ArabaExists(int id)
        {
            return _context.Arabalar.Any(e => e.Id == id);
        }
    }
}




