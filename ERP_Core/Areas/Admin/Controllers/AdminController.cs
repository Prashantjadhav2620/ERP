using Microsoft.AspNetCore.Mvc;

namespace ERP_Core.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
