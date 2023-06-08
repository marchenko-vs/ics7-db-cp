using BlitzFlug.Repositories;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BlitzFlug.Models
{
    public class Order
    {
        private IOrderRepository<Order> _db;

        public Int64 Id { get; set; }
        public Int64 UserId { get; set; }
        public string Status { get; set; }

        public Order()
        {
            this.Status = string.Empty;

            var factory = new MsSqlRepositoryFactory();
            this._db = factory.CreateOrderRepository();
        }

        public Order(IOrderRepository<Order> db)
        {
            this.Status = string.Empty;

            var factory = new MsSqlRepositoryFactory();
            this._db = db;
        }

        public Order(Int64 userId)
        {
            this.UserId = userId;
            this.Status = "создан";

            var factory = new MsSqlRepositoryFactory();
            this._db = factory.CreateOrderRepository();
        }

        public Order GetActiveOrderByUserId(Int64 userId)
        {
            return this._db.GetActiveOrderByUserId(userId);
        }

        public void TimeExperiment()
        {
            long time = 0;
            var timer = new Stopwatch();
            for (int i = 0; i < 100; i++)
            {
                timer.Start();
                this._db.GetOrderByUserId(1);
                timer.Stop();
                time += timer.ElapsedMilliseconds;
            }
            Console.WriteLine($"{time / 100} ms.");
            time = 0;
            for (int i = 0; i < 100; i++)
            {
                timer.Start();
                this._db.GetOrderByUserId(5000000);
                timer.Stop();
                time += timer.ElapsedMilliseconds;
            }
            Console.WriteLine($"{time / 100} ms.");
            time = 0;
            for (int i = 0; i < 100; i++)
            {
                timer.Start();
                this._db.GetOrderByUserId(10000000);
                timer.Stop();
                time += timer.ElapsedMilliseconds;
            }
            Console.WriteLine($"{time / 100} ms.");
        }

        public void AddTicketToOrder(Ticket ticket)
        {
            IEnumerable<Order> orders = this._db.GetOrderByUserId(this.UserId);
            List<Order> ordersList;

            if (0 == orders.Count() || "оплачен" == orders.ToList()[orders.ToList().Count - 1].Status)
            {
                this._db.InsertOrder(this.UserId);
            }

            ordersList = this._db.GetOrderByUserId(this.UserId).ToList();

            Int64 orderId = ordersList[ordersList.Count - 1].Id;
            ticket.AddTicket(orderId, ticket.Id);
        }

        public IEnumerable<OrderedTicket> GetOrder(Int64 userId)
        {
            return this._db.GetTicketsByOrderId(userId);
        }

        public void DeleteTicketFromOrder(Int64 ticketId)
        {
            this._db.DeleteTicketFromOrder(ticketId);
        }

        public void ClearOrder()
        {
            var currentUser = SingletonUser.GetInstance(new User());
            Int64 userId = currentUser.UserInfo.Id;

            IEnumerable<Order> orders = this._db.GetOrderByUserId(userId);
            List<Order> ordersList;

            if (null != orders && "оплачен" != orders.ToList()[orders.ToList().Count - 1].Status)
            {
                ordersList = this._db.GetOrderByUserId(userId).ToList();

                IEnumerable<OrderedTicket> tickets = this.GetOrder(userId);

                var newOrder = new Order();
                newOrder = orders.ToList()[orders.ToList().Count - 1];

                foreach (var ticket in tickets)
                {
                    var ticket_ = new Ticket();
                    ticket_.Id = ticket.Id;
                    ticket_.Available = true;
                    ticket_.UpdateTicket();
                }
            }
        }

        public void PurchaseOrder()
        {
            var currentUser = SingletonUser.GetInstance(new User());
            Int64 userId = currentUser.UserInfo.Id;

            IEnumerable<Order> orders = this._db.GetOrderByUserId(userId);
            List<Order> ordersList;

            if (null != orders && "оплачен" != orders.ToList()[orders.ToList().Count - 1].Status)
            {
                ordersList = this._db.GetOrderByUserId(userId).ToList();

                IEnumerable<OrderedTicket> tickets = this.GetOrder(userId);

                var newOrder = new Order();
                newOrder = orders.ToList()[orders.ToList().Count - 1];
                newOrder.Status = "оплачен";

                foreach (var ticket in tickets)
                {
                    var ticket_ = new Ticket();
                    ticket_.Id = ticket.Id;
                    newOrder.DeleteTicketFromOrder(ticket_.Id);
                }

                this._db.UpdateOrder(newOrder);
            }
        }

        public decimal GetOrderPrice(Int64 userId)
        { 
            return this._db.GetOrderPrice(userId);
        }
    }
}
