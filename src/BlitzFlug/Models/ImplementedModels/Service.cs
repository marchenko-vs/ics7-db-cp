using BlitzFlug.Repositories;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BlitzFlug.Models
{
    public class Service
    {
        private IServiceRepository<Service> _db;

        [Key]
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool EconomyClass { get; set; }
        public bool BusinessClass { get; set; }
        public bool FirstClass { get; set; }

        public Service()
        {
            this.Name = String.Empty;

            var factory = new MsSqlRepositoryFactory();
            this._db = factory.CreateServiceRepository();
        }

        public IEnumerable<Service> GetServicesByClass(string className)
        {
            IEnumerable<Service> services = new List<Service>();

            if ("эконом" == className)
            {
                services = this._db.GetEconomyClassServices();
            }
            else if ("первый" == className)
            {
                services = this._db.GetFirstClassServices();
            }
            else
            {
                services = this._db.GetBusinessClassServices();
            }

            return services;
        }
        public void AddService(Int64 ticketId, Int64 serviceId) 
        {
            try
            {
                this._db.InsertTicketsServices(ticketId, serviceId);
            }
            catch (Exception)
            {
                throw new Exception("Данная услуга уже выбрана для текущего билета");
            }
        }

        public IEnumerable<Service> GetActiveServices(Int64 ticketId)
        {
            IEnumerable<Service> services = new List<Service>();

                services = this._db.GetServicesByTicketId(ticketId);

            return services;
        }

        public void DeleteFromTicket(Int64 ticketId, Int64 serviceId)
        { 
            this._db.DeleteFromTicket(ticketId, serviceId);
        }
    }
}
