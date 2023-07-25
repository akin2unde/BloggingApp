using BloggingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BloggingApp.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly BloggingDBContext DBContext;
        private readonly UserManager<AppUser> _securityManager;
        private readonly SignInManager<AppUser> _loginManager;
        public BlogController(BloggingDBContext dBContext,UserManager<AppUser> secMgr, SignInManager<AppUser> loginManager)
        {
                DBContext = dBContext;
                _securityManager = secMgr;
                _loginManager = loginManager;
                DBContext = dBContext;
        }

        public IActionResult Index()
        {
          
            var myPosts = DBContext.Blogs.Where(f => f.UserMailAddress == User.Identity.Name).OrderByDescending(f => f.PublicationDate).ToList();
            return View(myPosts);
        }
        [HttpGet]
        public IActionResult NewPost()
        {
            return View();
        } 
        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            HttpClient _httpClient = new HttpClient();
            var response = await _httpClient.GetAsync("https://mocki.io/v1/d33691f7-1eb5-45aa-9642-8d538f6c5ebd");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var jsonObj = JsonConvert.DeserializeObject<dynamic>(content);
            var data = jsonObj.data as IEnumerable<dynamic>;
            CultureInfo provider = CultureInfo.InvariantCulture;
            var blogs = data.Select(x => new Blog { 
                UserMailAddress= User.Identity.Name,
                CreatedAt= DateTime.Now, 
                Description= x.description,
                Title=x.title, 
                PublicationDate = Convert.ChangeType(x.publication_date, typeof(DateTime), System.Globalization.CultureInfo.GetCultureInfo("en-GB"))
            });
            if (blogs.Any())
            {
                foreach (var item in blogs)
                {
                    var found = await DBContext.Blogs.FirstOrDefaultAsync(f => f.Title == item.Title);
                    if (found == null)
                    {
                        DBContext.Blogs.Add(item);
                    }
                }
                await DBContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> NewPost(Blog data)
        {
            var found =await DBContext.Blogs.FirstOrDefaultAsync(f=>f.Title== data.Title);
            if (found == null)
            {
                data.PublicationDate = DateTime.Now;
                data.UserMailAddress = User.Identity.Name;
                DBContext.Blogs.Add(data);
                await  DBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("New");
        }
    }
}
