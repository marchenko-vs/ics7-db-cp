using BlitzFlug.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;

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
            User user = new User();
            var singletonUser = SingletonUser.GetInstance();

            return View(user.GetCurrentUser(singletonUser.UserInfo.Email));
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
                var singletonUser = SingletonUser.GetInstance();
                return View(user.GetCurrentUser(singletonUser.UserInfo.Email));
            }

            TempData["Info"] = "Данные изменены. Войдите в личный кабинет";
            return RedirectToAction("LogOut");
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
                ViewData["ValidateMessage"] = "Данный электронный адрес уже используется";
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
        public ActionResult FindUsers()
        {
            var user = new User();
            List<User> users = user.GetAllUsers();

            return View(users);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult HandleUsers()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult FindUser(User user)
        {
            return View(user.GetById());
        }
    }
}
