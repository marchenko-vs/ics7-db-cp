using BlitzFlug.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlitzFlug.Controllers
{
    public class TicketsController : Controller
    {
        public IActionResult Index(Flight flight)
        {
            var ticket = new Ticket();
            List<Ticket> tickets = new List<Ticket>();
            
            tickets = ticket.GetAvailableTickets(flight.Id).ToList();

            return View(tickets);
        }

        public IActionResult BookTicket(Ticket ticket)
        {
            ClaimsPrincipal user = HttpContext.User;

            if (!user.Identity.IsAuthenticated)
                return RedirectToAction("Register", "Users");

            ticket = ticket.GetTicketById();
            ticket.BookTicket();
            List<Ticket> tickets = ticket.GetAvailableTickets(ticket.FlightId).ToList();

            return View("Index", tickets);
        }

        public IActionResult GetFlightsByClass(Int64 flightId, string className)
        {
            var ticket = new Ticket();
            List<Ticket> tickets = ticket.GetTicketsByClass(flightId, className).ToList();

            return View("Index", tickets);
        }

        [Authorize(Roles = "moderator, admin")]
        [HttpGet]
        public IActionResult HandleTickets()
        {
            var ticket = new Ticket();
            List<Ticket> tickets = ticket.GetAllTickets().ToList();

            return View(tickets);
        }

        [Authorize(Roles = "moderator, admin")]
        public IActionResult DeleteTicket(Ticket ticket)
        {
            ticket.DeleteTicket();

            return RedirectToAction("HandleTickets");
        }

        [Authorize(Roles = "moderator, admin")]
        [HttpGet]
        public IActionResult InsertTicket()
        {
            return View();
        }

        [Authorize(Roles = "moderator, admin")]
        public IActionResult InsertTicket(Ticket ticket)
        {
            try
            {
                ticket.InsertTicket();
            }
            catch (Exception) 
            {
                ViewData["Error"] = "Указаны некорректные данные";

                return View();
            }

            return RedirectToAction("HandleTickets");
        }
    }
}
