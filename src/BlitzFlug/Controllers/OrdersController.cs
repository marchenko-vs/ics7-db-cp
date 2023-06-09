using BlitzFlug.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlitzFlug.Controllers
{
    public class OrdersController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var currentUser = SingletonUser.GetInstance();
            var order = new Order();
            IEnumerable<OrderedTicket> tickets = new List<OrderedTicket>();

            order = order.GetActiveOrderByUserId(currentUser.UserInfo.Id);

            if (null != order)
            {
                tickets = order.GetOrder(currentUser.UserInfo.Id);
                ViewData["Order"] = order.GetOrderPrice(order.Id).ToString("F");
            }

            return View(tickets);
        }

        public IActionResult CancelBooking(OrderedTicket orderedTicket)
        {
            var ticket = new Ticket
            {
                Id = orderedTicket.Id,
                OrderId = 0
            };
            ticket.UpdateTicket();

            return RedirectToAction("Index", "Orders");
        }

        public IActionResult ClearOrder()
        {
            var order = new Order();
            order.ClearOrder();

            return RedirectToAction("Index", "Orders");
        }

        public IActionResult Purchase()
        {
            var order = new Order();
            order.PurchaseOrder();

            return View("Purchased");
        }

        public IActionResult OrdersHistory()
        {
            var currentUser = SingletonUser.GetInstance();
            var order = new Order() { UserId = currentUser.UserInfo.Id };

            return View(order.GetOrdersHistory().ToList());
        }

        public IActionResult ShowOrderDetails(Order order)
        {
            return View(order.GetOrderById().ToList());
        }

        public IActionResult IssueRefund(OrderedTicket orderedTicket)
        {
            var ticket = new Ticket
            {
                Id = orderedTicket.Id,
                OrderId = 0
            };
            ticket.UpdateTicket();

            return RedirectToAction("OrdersHistory");
        }
    }
}
