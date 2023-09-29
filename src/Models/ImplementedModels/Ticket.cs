using BlitzFlug.Repositories;
using System.ComponentModel.DataAnnotations;

namespace BlitzFlug.Models
{
    public class Ticket
    {
        private ITicketRepository<Ticket> _db;

        [Key]
        public Int64 Id { get; set; }
        public Int64 FlightId { get; set; }
        public Int64 OrderId { get; set; }
        public int Row { get; set; }
        public char Place { get; set; }
        public string Class { get; set; }
        public bool Refund { get; set; }
        public Decimal Price { get; set; }

        public Ticket()
        {
            this.Class = string.Empty;

            var factory = new MsSqlRepositoryFactory();
            this._db = factory.CreateTicketRepository();
        }

        public Ticket(ITicketRepository<Ticket> db)
        {
            this.Class = string.Empty;
            this._db = db;
        }

        public Ticket GetTicketById()
        {
            return this._db.GetTicketById(this.Id);
        }

        public IEnumerable<Ticket> GetTicketsByClass(Int64 flightId, string className)
        {
            return this._db.GetTicketsByClass(flightId, className);
        }

        public IEnumerable<Ticket> GetAvailableTickets(Int64 flightId)
        {
            List<Ticket> tickets = new List<Ticket>();

            try
            {
                tickets = this._db.GetTicketsByFlightId(flightId).ToList();
            }
            catch (Exception)
            {
                throw new NoTicketsException("Билеты не найдены!");
            }

            foreach (var ticket in tickets.ToList())
            {
                if (0 != ticket.OrderId)
                {
                    tickets.Remove(ticket);
                }
            }

            return tickets;
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            return this._db.GetAllTickets().ToList();
        }

        public IEnumerable<Ticket> GetByFlightId()
        {
            return this._db.GetTicketsByFlightId(this.FlightId);
        }

        public void BookTicket()
        {
            var currentUser = SingletonUser.GetInstance(new User());
            var order = new Order(currentUser.UserInfo.Id);

            order.AddTicketToOrder();
            order = order.GetActiveOrderByUserId(order.UserId);

            this.OrderId = order.Id;
            this._db.UpdateTicket(this);
        }

        public void DeleteTicket()
        {
            this._db.DeleteTicket(this.Id);
        }

        public void UpdateTicket()
        {
            this._db.UpdateTicket(this);

            if (0 == this.OrderId)
            {
                var order = new Order();
                order.DeleteTicketFromOrder(this.Id);
            }
        }

        public void InsertTicket()
        {
            this._db.InsertTicket(this);
        }
    }
}
