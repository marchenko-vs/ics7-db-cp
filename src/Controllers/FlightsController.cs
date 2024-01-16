using BlitzFlug.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BlitzFlug.Controllers
{
    public class FlightsController : Controller
    {
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (null != claims)
            {
                var email = claims.Value;
                var currentUser = new User();
                var singletonUser = SingletonUser.GetInstance(currentUser.GetCurrentUser(email));
            }

            var flight = new Flight();

            return View(flight.GetUniquePoints().ToList());
        }

        [HttpGet]
        public IActionResult FindFlights(Flight flight)
        {
            if (flight.DeparturePoint == flight.ArrivalPoint)
            {
                TempData["Message"] = "Укажите разные города";
                return RedirectToAction("Index");
            }

            List<Flight> flights = new List<Flight>();

            try
            { 
                flights = flight.GetFlights().ToList();
            }
            catch (Exception) 
            {
                TempData["Message"] = "Укажите корректную дату вылета";
                return RedirectToAction("Index");
            }

            return View("FoundFlights", flights);
        }

        [HttpGet]
        public IActionResult FindTickets()
        {
            return RedirectToAction("Index", "Tickets");
        }

        [Authorize(Roles = "moderator, admin")]
        [HttpGet]
        public IActionResult HandleFlights()
        {
            return View();
        }

        [Authorize(Roles = "moderator, admin")]
        [HttpGet]
        public IActionResult FindFlight(Flight flight)
        {
            return View(flight.GetById());
        }

        [Authorize(Roles = "moderator, admin")]
        [HttpGet]
        public IActionResult FindAllFlights()
        {
            var flight = new Flight();

            return View(flight.GetAllFlights().ToList());
        }

        [Authorize(Roles = "moderator, admin")]
        public IActionResult DeleteFlight(Flight flight)
        {
            flight.DeleteFlight();

            return RedirectToAction("HandleFlights");
        }

        [Authorize(Roles = "moderator, admin")]
        [HttpGet]
        public IActionResult InsertFlight()
        {
            return View();
        }

        [Authorize(Roles = "moderator, admin")]
        public IActionResult InsertFlight(Flight flight)
        {
            try
            {
                flight.InsertFlight();
            }
            catch (Exception)
            {
                ViewData["Error"] = "Введены некорректные данные";
                return View();
            }

            return RedirectToAction("HandleFlights");
        }
    }
}
