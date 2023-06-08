using BlitzFlug.Data;
using BlitzFlug.Models;
using Microsoft.Data.SqlClient;

namespace BlitzFlug.Repositories
{
    public class ServiceRepository : IServiceRepository<Service>
    {
        private string _connectionString;
        private string _connectionInfo;
        private string _userInfo;

        public ServiceRepository()
        {
            this._connectionInfo = Startup.ConnectionString;
            var currentUser = SingletonUser.GetInstance();

            if (null == currentUser)
            {
                this._userInfo = Startup.CustomerData;
            }
            else if ("customer" == currentUser.UserInfo.Role)
            {
                this._userInfo = Startup.CustomerData;
            }
            else if ("moderator" == currentUser.UserInfo.Role)
            {
                this._userInfo = Startup.ModeratorData;
            }
            else
            {
                this._userInfo = Startup.AdminData;
            }

            this._connectionString = this._connectionInfo + this._userInfo;
        }

        public Service GetServiceById(Int64 serviceId)
        {
            List<Service> services = new List<Service>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Services] where Id = @serviceId", connection);
                command.Parameters.AddWithValue("serviceId", serviceId);

                var dataReader = command.ExecuteReader();

                services = QueryHandler.GetList<Service>(dataReader);

                if (0 == services.Count)
                    return null;
            }

            return services[0];
        }

        public void InsertTicketsServices(Int64 ticketId, Int64 serviceId)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"INSERT INTO [TicketsServices] (TicketId, serviceId) VALUES " +
                    $"(@ticketId, @serviceId)", connection);

                command.Parameters.AddWithValue("ticketId", ticketId);
                command.Parameters.AddWithValue("serviceId", serviceId);

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Service> GetBusinessClassServices()
        {
            List<Service> services = new List<Service>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Services] WHERE BusinessClass = 1", connection);

                var dataReader = command.ExecuteReader();

                services = QueryHandler.GetList<Service>(dataReader);
            }

            return services;
        }

        public IEnumerable<Service> GetServicesByTicketId(Int64 ticketId)
        {
            List<Service> services = new List<Service>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT s.Id, s.Name, s.Price, s.EconomyClass, " +
                    $"s.FirstClass, s.BusinessClass FROM [TicketsServices] ts JOIN [Services] s " +
                    $"ON s.Id = ts.ServiceId WHERE ts.TicketId = @ticketId", connection);

                command.Parameters.AddWithValue("ticketId", ticketId);

                var dataReader = command.ExecuteReader();

                services = QueryHandler.GetList<Service>(dataReader);
            }

            return services;
        }

        public void DeleteFromTicket(Int64 ticketId, Int64 serviceId)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"DELETE FROM [TicketsServices] " +
                    $"WHERE TicketId = @ticketId AND ServiceId = @serviceId", connection);

                command.Parameters.AddWithValue("TicketId", ticketId);
                command.Parameters.AddWithValue("ServiceId", serviceId);

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Service> GetEconomyClassServices()
        {
            List<Service> services = new List<Service>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Services] WHERE EconomyClass = 1", connection);

                var dataReader = command.ExecuteReader();

                services = QueryHandler.GetList<Service>(dataReader);
            }

            return services;
        }

        public IEnumerable<Service> GetFirstClassServices()
        {
            List<Service> services = new List<Service>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Services] WHERE FirstClass = 1", connection);

                var dataReader = command.ExecuteReader();

                services = QueryHandler.GetList<Service>(dataReader);
            }

            return services;
        }

        public void InsertService(Service service)
        {
            throw new NotImplementedException();
        }

        public void UpdateService(Service service)
        {
            throw new NotImplementedException();
        }

        public void DeleteService(long serviceId)
        {
            throw new NotImplementedException();
        }
    }
}
