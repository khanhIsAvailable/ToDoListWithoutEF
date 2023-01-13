using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoListWithoutEF.Models;

namespace ToDoListWithoutEF.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}