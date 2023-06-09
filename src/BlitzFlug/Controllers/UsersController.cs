using BlitzFlug.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlitzFlug.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Settings(User user)
        {
            try
            {
                user.ChangeProfile();
            }
            catch (NotLoggedInException)
            {
                return RedirectToAction("LogOut", "Users");
            }
            catch (Exception ex)
            {
                ViewData["SettingsError"] = ex.Message;

                return View();
            }

            return RedirectToAction("LogOut", "Users");
        }

        [HttpPost]
        public ActionResult Register(string email, string password)
        {
            var user = new User();

            try
            {
                user.Register(email, password);
            }
            catch (Exception) 
            {
                ViewData["ValidateMessage"] = "Пользователь с такой почтой уже зарегистрирован!";
                return View();
            }

            return View("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            ClaimsPrincipal user = HttpContext.User;

            if (user.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Flights");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string email, string password)
        {
            var user = new User();

            try
            {
                user.Login(email, password);
            }
            catch (Exception ex) 
            {
                ViewData["ValidateMessage"] = ex.Message;

                return View();
            }

            var currentUser = user.GetCurrentUser(email);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, email));
            claims.Add(new Claim(ClaimTypes.Role, currentUser.Role));

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties() 
            { AllowRefresh = false };
                
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(identity), properties);

            var singletonUser = SingletonUser.GetInstance(currentUser);
            singletonUser.UserInfo.Id = currentUser.Id;
            singletonUser.UserInfo.Role = currentUser.Role;
            singletonUser.UserInfo.Email = currentUser.Email;
            singletonUser.UserInfo.FirstName = currentUser.FirstName;
            singletonUser.UserInfo.LastName = currentUser.LastName;
            singletonUser.UserInfo.RegDate = currentUser.RegDate;
            
            return RedirectToAction("Index", "Flights");
        }

        public async Task<ActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Users");
        }

        [Authorize(Roles = "moderator, admin")]
        [HttpGet]
        public ActionResult AdminPanel()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult BlockUser(User user)
        {
            user.DeleteUser();

            return RedirectToAction("HandleUsers");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult HandleUsers()
        {
            var user = new User();
            List<User> users = user.GetAllUsers();

            return View(users);
        }
    }
}
