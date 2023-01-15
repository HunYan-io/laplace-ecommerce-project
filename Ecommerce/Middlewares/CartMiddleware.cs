using Ecommerce.Models;
using Newtonsoft.Json;
using System.Text;

namespace Ecommerce.Middlewares
{
    public class CartMiddleware
    {
        private RequestDelegate _next;

        public CartMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            List<CartItem> cart;
            if (context.Session.TryGetValue("cart", out byte[] cartBytes))
            {
                cart = JsonConvert.DeserializeObject<List<CartItem>>(Encoding.UTF8.GetString(cartBytes))!;
            }
            else
            {
                cart = new List<CartItem>();
            }
            context.Items["Cart"] = cart;
            await _next(context);
        }
    }
}
