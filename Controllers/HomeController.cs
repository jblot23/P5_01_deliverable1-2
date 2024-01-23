using EDDIESCARDEALAERSHIP.Data;
using EDDIESCARDEALAERSHIP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;

namespace EDDIESCARDEALAERSHIP.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        //public HomeController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        //private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            Initialize();
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync("Admin").Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();
            }
            else { return; }

            ApplicationUser admin = new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "eddie@gmail.com",
                Email = "eddie@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111111",
                isAccess = true,
            };

            _userManager.CreateAsync(admin, "eddie").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();

        }
        public IActionResult Index(decimal? minPrice, decimal? maxPrice)
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
