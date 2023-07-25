using BloggingApp.Models;
using BloggingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;
namespace BloggingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly BloggingDBContext DBContext;


        public HomeController(BloggingDBContext dBContext)
        {
            DBContext = dBContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var loadBlogs = DBContext.Blogs.OrderByDescending(f=>f.PublicationDate).ToList();
            return View(loadBlogs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}