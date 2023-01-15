using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ApplicationDbContext context)
        {
            _productRepository = new ProductRepository(context);
        }

        public IActionResult Index(int id, bool? success)
        {
            Product? product = _productRepository.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            if (success == true)
            {
                ViewData["Alert"] = "Item was added to cart successfully!";
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            List<CartItem> cart = (List<CartItem>)HttpContext.Items["Cart"]!;
            var item = cart.Find(item => item.ProductId == id);
            var found = item != null;
            item = item ?? new CartItem();
            if (found)
            {
                item.Quantity += quantity;
            }
            else
            {
                item.ProductId = id;
                item.Quantity = quantity;
                cart.Add(item);
            }
            HttpContext.Session.Set("cart", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cart)));
            HttpContext.Items["Cart"] = cart;
            return RedirectToAction("Index", new {id, success=true});
        }
    }
}
