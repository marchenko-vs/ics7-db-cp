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
            Console.WriteLine(flight.DepartureDateTime);
            List<Flight> flights = flight.GetFlights().ToList();

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
            var flight = new Flight();
            List<Flight> flights = flight.GetAllFlights().ToList();

            return View(flights);
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
            flight.InsertFlight();

            return RedirectToAction("HandleFlights");
        }
    }
}
