using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArabaGaleri.Data;
using ArabaGaleri.Models;
using ArabaGaleri.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ArabaGaleri.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Kullanicilar
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user != null && PasswordHasher.VerifyPassword(model.Sifre, user.Sifre))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.KullaniciAdi),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };

                    if (user.IsAdmin)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    if (user.IsAdmin)
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı.");
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Email kontrolü
                if (await _context.Kullanicilar.AnyAsync(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor.");
                    return View(model);
                }

                // Kullanıcı adı kontrolü
                if (await _context.Kullanicilar.AnyAsync(u => u.KullaniciAdi == model.KullaniciAdi))
                {
                    ModelState.AddModelError("KullaniciAdi", "Bu kullanıcı adı zaten kullanılıyor.");
                    return View(model);
                }

                var user = new Kullanici
                {
                    KullaniciAdi = model.KullaniciAdi,
                    Email = model.Email,
                    Sifre = PasswordHasher.HashPassword(model.Sifre),
                    IsAdmin = false
                };

                _context.Kullanicilar.Add(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Kayıt başarılı! Lütfen giriş yapın.";
                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}




