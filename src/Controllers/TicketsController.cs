using BlitzFlug.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlitzFlug.Controllers
{
    public class TicketsController : Controller
    {
        public IActionResult Index(Flight flight)
        {
            var ticket = new Ticket();          

            return View(ticket.GetAvailableTickets(flight.Id).ToList());
        }

        [Authorize]
        public IActionResult BookTicket(Ticket ticket)
        {
            ticket = ticket.GetTicketById();

            try
            {
                ticket.BookTicket();
            }
            catch (Exception)
            {
                return RedirectToAction("Register", "Users");
            }

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
        public IActionResult AFindTickets()
        {
            var ticket = new Ticket();

            return View(ticket.GetAllTickets().ToList());
        }

        [Authorize(Roles = "moderator, admin")]
        [HttpGet]
        public IActionResult HandleTickets()
        {
            return View();
        }

        [Authorize(Roles = "moderator, admin")]
        [HttpGet]
        public IActionResult AFindTicket(Ticket ticket)
        {
            return View(ticket.GetTicketById());
        }

        [Authorize(Roles = "moderator, admin")]
        [HttpGet]
        public IActionResult AFindTicketsByFlight(Ticket ticket)
        {
            return View(ticket.GetByFlightId());
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
