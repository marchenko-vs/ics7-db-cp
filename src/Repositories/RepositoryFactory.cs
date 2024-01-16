using BlitzFlug.Data;
using BlitzFlug.Models;

namespace BlitzFlug.Repositories
{
    public abstract class RepositoryFactory
    {
        public abstract IUserRepository<User> CreateUserRepository();
        public abstract IFlightRepository<Flight> CreateFlightRepository();
        public abstract IPlaneRepository<Plane> CreatePlaneRepository();
        public abstract IServiceRepository<Service> CreateServiceRepository();
        public abstract ITicketRepository<Ticket> CreateTicketRepository();
        public abstract IOrderRepository<Order> CreateOrderRepository();
    }

    public class MsSqlRepositoryFactory : RepositoryFactory
    {
        public override FlightRepository CreateFlightRepository()
        {
            return new FlightRepository();
        }

        public override PlaneRepository CreatePlaneRepository()
        {
            return new PlaneRepository();
        }

        public override ServiceRepository CreateServiceRepository()
        {
            return new ServiceRepository();
        }

        public override TicketRepository CreateTicketRepository()
        {
            return new TicketRepository();
        }

        public override UserRepository CreateUserRepository()
        {
            return new UserRepository();
        }

        public override OrderRepository CreateOrderRepository()
        {
            return new OrderRepository();
        }
    }
}
