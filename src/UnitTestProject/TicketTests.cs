using BlitzFlug.Models;
using Moq;
using BlitzFlug.Repositories;

namespace BlitzFlugUnitTests
{
    public class TicketTests
    {
        [Fact]
        public void AllTicketsAvailable()
        {
            Int64 flightId = 1;
            var mock = new Mock<ITicketRepository<Ticket>>();

            mock.Setup(repo => repo.GetTicketsByFlightId(flightId)).Returns(AllAvailable(flightId));

            var ticket = new Ticket(mock.Object);
            List<Ticket> tickets = ticket.GetAvailableTickets(flightId).ToList();

            Assert.Equal(4, tickets.Count);
        }

        [Fact]
        public void NotAllTicketsAvailable()
        {
            Int64 flightId = 1;
            var mock = new Mock<ITicketRepository<Ticket>>();

            mock.Setup(repo => repo.GetTicketsByFlightId(flightId)).Returns(NotAllAvailable(flightId));

            var ticket = new Ticket(mock.Object);

            List<Ticket> tickets = ticket.GetAvailableTickets(flightId).ToList();

            Assert.Equal(2, tickets.Count);
        }

        [Fact]
        public void NoTicketsAvailable()
        {
            Int64 flightId = 1;
            var mock = new Mock<ITicketRepository<Ticket>>();

            mock.Setup(repo => repo.GetTicketsByFlightId(flightId)).Returns(NoAvailable(flightId));

            var ticket = new Ticket(mock.Object);

            List<Ticket> tickets = ticket.GetAvailableTickets(flightId).ToList();

            Assert.Empty(tickets);
        }

        private IEnumerable<Ticket> AllAvailable(Int64 flightId)
        {
            return new List<Ticket>() {
                new Ticket() { Id = 1, FlightId = 1, Available = true },
                new Ticket() { Id = 2, FlightId = 1, Available = true },
                new Ticket() { Id = 3, FlightId = 1, Available = true },
                new Ticket() { Id = 4, FlightId = 1, Available = true },
            };
        }

        private IEnumerable<Ticket> NotAllAvailable(Int64 flightId)
        {
            return new List<Ticket>() {
                new Ticket() { Id = 1, FlightId = 1, Available = true },
                new Ticket() { Id = 2, FlightId = 1, Available = false },
                new Ticket() { Id = 3, FlightId = 1, Available = true },
                new Ticket() { Id = 4, FlightId = 1, Available = false },
            };
        }

        private IEnumerable<Ticket> NoAvailable(Int64 flightId)
        {
            return new List<Ticket>() {
                new Ticket() { Id = 1, FlightId = 1, Available = false },
                new Ticket() { Id = 2, FlightId = 1, Available = false },
                new Ticket() { Id = 3, FlightId = 1, Available = false },
                new Ticket() { Id = 4, FlightId = 1, Available = false },
            };
        }
    }
}
