using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Models;
using Newtonsoft.Json;
using System.Text;
namespace Ecommerce.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminRepository _adminRepository;
        private readonly ProductRepository _productRepository;
        private readonly CommandRepository _commandRepository;
        private readonly CommandItemRepository _commandItemRepository;

        public AdminController(ApplicationDbContext context)
        {
            _adminRepository = new AdminRepository(context);
            _productRepository = new ProductRepository(context);
            _commandRepository =new CommandRepository(context);
            _commandItemRepository =new CommandItemRepository(context);
        }
        public IActionResult Index()
        {
            bool isAdmin = (bool)HttpContext.Items["IsAdmin"]!;
            if (!isAdmin)
            {
                return RedirectToAction("Login");
            }
            List<Command> commands = _commandRepository.GetAll().ToList();
            ViewData["Commands"] = commands;
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            bool isAdmin = (bool)HttpContext.Items["IsAdmin"]!;
            if (isAdmin)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin admin)
        {   
            if (_adminRepository.Authenticate(admin.Email, admin.Password))
            {
                HttpContext.Session.Set("isAdmin", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(true)));
                HttpContext.Items["IsAdmin"] = true;
                return RedirectToAction("Index");
            } else
            {
                ViewData["Error"] = "Invalid email/password combination.";
                return View();
            }
        }
        [HttpPost] 
        public IActionResult Product(Product p)
        {
            _productRepository.Add(p);
            return RedirectToAction("Index");
        }
     }
}