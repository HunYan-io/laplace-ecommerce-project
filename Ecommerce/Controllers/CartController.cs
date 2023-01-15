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
    public class CartController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CommandRepository _commandRepository;
        private readonly CommandItemRepository _commandItemRepository;
        
        public CartController(ApplicationDbContext context)
        {
            _productRepository = new ProductRepository(context);
            _commandRepository = new CommandRepository(context);
            _commandItemRepository = new CommandItemRepository(context);
        }
        public IActionResult Index(bool? success)
        {
            List<CartItem> cart = (List<CartItem>)HttpContext.Items["Cart"]!;
            List<dynamic> items = new List<dynamic>();
            var productIds = cart.Select(item => item.ProductId);
            var products = _productRepository.GetByIds(productIds);
            foreach (var cartItem in cart)
            {
                items.Add(new { Product = products.Where(product => product.Id == cartItem.ProductId).First(), Quantity = cartItem.Quantity });
            }
            ViewData["Items"] = items;
            ViewData["Count"] = cart.Count;
            int totalPrice = 0;
            foreach (var item in items)
            {
                totalPrice += item.Product.Price * item.Quantity;
            }
            ViewData["TotalPrice"] = totalPrice;
            if (success == true)
            {
                ViewData["Alert"] = "Your command was submitted successfully. We will get in touch with you as soon as possible.";
            }
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            List<CartItem> cart = (List<CartItem>)HttpContext.Items["Cart"]!;
            cart = cart.FindAll(item => item.ProductId != id);
            HttpContext.Session.Set("cart", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cart)));
            HttpContext.Items["Cart"] = cart;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Command(Command c)
        {
            _commandRepository.Add(c);
            List<CartItem> cart = (List<CartItem>)HttpContext.Items["Cart"]!;
            List<CommandItem> commandItems = new List<CommandItem>();
            foreach(var item in cart)
            {
                var commandItem = new CommandItem();
                commandItem.CommandId = c.Id;
                commandItem.ProductID = item.ProductId;
                commandItem.Quantity = item.Quantity;
                commandItems.Add(commandItem);
            }
            _commandItemRepository.AddAll(commandItems);
            cart = new List<CartItem>();
            HttpContext.Session.Set("cart", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cart)));
            HttpContext.Items["Cart"] = cart;
            return RedirectToAction("Index", new { success = true });
        }
    }
   
}
