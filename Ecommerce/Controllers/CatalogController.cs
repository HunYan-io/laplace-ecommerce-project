using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ProductRepository _productRepository;

        public CatalogController(ApplicationDbContext context)
        {
            _productRepository = new ProductRepository(context);
        }

        public IActionResult Index(int page = 1)
        {
            ViewData["Page"] = page;
            ViewData["PageCount"] = _productRepository.GetPageCount();
            return View(_productRepository.GetProductsForPage(page));
        }
    }
}
