using BlitzFlug.Models;
using System.Collections;

namespace BlitzFlug.Repositories
{
    public interface IServiceRepository<T>
        where T : class
    {
        public Service GetServiceById(Int64 serviceId);
        public IEnumerable<T> GetEconomyClassServices();
        public IEnumerable<T> GetFirstClassServices();
        public IEnumerable<T> GetBusinessClassServices();
        public IEnumerable<T> GetServicesByTicketId(Int64 ticketId);
        public void InsertTicketsServices(Int64 ticketId, Int64 serviceId);
        public void DeleteFromTicket(Int64 ticketId, Int64 serviceId);
        public void InsertService(T service);
        public void UpdateService(T service);
        public void DeleteService(Int64 serviceId);
    }
}
