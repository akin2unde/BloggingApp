using BloggingApp.Models;
using BloggingApp.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BloggingApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<AppUser> _securityManager;
        private readonly SignInManager<AppUser> _loginManager;
        BloggingDBContext DBContext;
        public AuthenticationController(BloggingDBContext dBContext, 
            UserManager<AppUser> secMgr, SignInManager<AppUser> loginManager)
        {
            DBContext= dBContext;
            _securityManager = secMgr;
            _loginManager = loginManager;
        }
        //Login
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_loginManager.IsSignedIn(User))
            {
                await _loginManager.SignOutAsync();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(UserWrapperVM userWrapperVM)
        {
            var res = await DBContext.Users.FirstOrDefaultAsync(f => f.MailAddress == userWrapperVM.User.MailAddress);
            if (res == null)
            {
                userWrapperVM.User.Id = Guid.NewGuid();
                userWrapperVM.User.Password = Util.HashPasword(userWrapperVM.User.Password);
                userWrapperVM.User.PasswordHash = userWrapperVM.User.Password;
                userWrapperVM.User.CreatedAt= DateTime.Now;
                userWrapperVM.User.UpdatedAt= DateTime.Now;
                userWrapperVM.User.UserName = userWrapperVM.User.MailAddress;
                DBContext.Users.Add(userWrapperVM.User);
                await DBContext.SaveChangesAsync();
                if (_loginManager.IsSignedIn(User))
                {
                await _loginManager.SignOutAsync();
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Signup");
        }
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await _loginManager.SignOutAsync();
            return RedirectToAction("Index");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM wrapperVM)
        {

           var pswHash =Util.HashPasword(wrapperVM.Password);
           var res= await DBContext.Users.FirstOrDefaultAsync(f => f.MailAddress == wrapperVM.MailAddress && f.Password== pswHash);
            if (res != null)
            {
                await _loginManager.SignInAsync(new AppUser { UserName= res.MailAddress,Email=res.MailAddress, PasswordHash= pswHash }, isPersistent: false);
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("Index");
        }
    }
}
