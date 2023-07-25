using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Blogging_Platform.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
       
        public IActionResult Display()
        {
            return View();
        }
    }
}
