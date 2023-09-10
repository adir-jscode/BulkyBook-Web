using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    public class CartController : Controller
    {

        [Area("Customer")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
