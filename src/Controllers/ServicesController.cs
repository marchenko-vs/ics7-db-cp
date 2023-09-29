using BlitzFlug.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlitzFlug.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index(Ticket ticket)
        {
            var service = new Service();
            List<Service> services = service.GetServicesByClass(ticket.Class).ToList();

            ViewData["TicketId"] = ticket.Id;
            
            return View(services);
        }

        public IActionResult AddService(Int64 ticketId, Int64 serviceId)
        {
            var service = new Service();
            var ticket = new Ticket { Id = ticketId };

            try
            {
                service.AddService(ticketId, serviceId);
            }
            catch (Exception) 
            {
                TempData["Warning"] = "Услуга уже добавлена";
            }

            return RedirectToAction("Index", ticket.GetTicketById());
        }

        public IActionResult ActiveServices(Ticket ticket)
        {
            var service = new Service();

            ViewData["ticketId"] = ticket.Id;

            return View(service.GetActiveServices(ticket.Id).ToList());
        }

        public IActionResult PurchasedServices(Ticket ticket)
        {
            var service = new Service();

            ViewData["ticketId"] = ticket.Id;

            return View(service.GetActiveServices(ticket.Id).ToList());
        }

        public IActionResult DeleteFromTicket(Int64 ticketId, Int64 serviceId)
        {
            var service = new Service();
            service.DeleteFromTicket(ticketId, serviceId);

            return RedirectToAction("Index", "Orders");
        }
    }
}
