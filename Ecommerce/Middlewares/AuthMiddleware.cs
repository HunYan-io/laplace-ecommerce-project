using Ecommerce.Models;
using Newtonsoft.Json;
using System.Text;

namespace Ecommerce.Middlewares
{
    public class AuthMiddleware
    {
        private RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
           bool isAdmin;
            if (context.Session.TryGetValue("isAdmin", out byte[] isAdminBytes))
            {
                isAdmin = JsonConvert.DeserializeObject<bool>(Encoding.UTF8.GetString(isAdminBytes))!;
            }
            else
            {
                isAdmin = false;
            }
            context.Items["IsAdmin"] = isAdmin;
            await _next(context);
        }
    }
}
