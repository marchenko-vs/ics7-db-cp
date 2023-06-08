using BlitzFlug.Models;

namespace BlitzFlug.Repositories
{
    public interface ITicketRepository<T> 
        where T : class
    {
        public IEnumerable<T> GetAllTickets();
        public T GetTicketById(Int64 id);
        public IEnumerable<T> GetTicketsByClass(Int64 flightId, string className);
        public IEnumerable<T> GetTicketsByFlightId(Int64 flightId);
        public void InsertTicket(T ticket);
        public void UpdateTicket(T ticket);
        public void DeleteTicket(Int64 ticketId);
    }
}
