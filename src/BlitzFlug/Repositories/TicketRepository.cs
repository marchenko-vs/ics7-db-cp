using BlitzFlug.Data;
using BlitzFlug.Models;
using Microsoft.Data.SqlClient;

namespace BlitzFlug.Repositories
{
    public class TicketRepository : ITicketRepository<Ticket>
    {
        private string _connectionString;
        private string _connectionInfo;
        private string _userInfo;

        public TicketRepository()
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

        public Ticket GetTicketById(Int64 id)
        {
            List<Ticket> tickets = new List<Ticket>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Tickets] where [Tickets].Id = @Id", connection);
                command.Parameters.AddWithValue("Id", id);

                var dataReader = command.ExecuteReader();

                tickets = QueryHandler.GetList<Ticket>(dataReader);
            }

            if (0 == tickets.Count)
                return null;

            return tickets[0];
        }

        public IEnumerable<Ticket> GetTicketsByFlightId(Int64 flightId)
        {
            List<Ticket> tickets = new List<Ticket>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Tickets] WHERE " +
                    $"[Tickets].FlightId = @FlightId ORDER BY Row, Place, Id", connection);
                command.Parameters.AddWithValue("FlightId", flightId);

                var dataReader = command.ExecuteReader();

                tickets = QueryHandler.GetList<Ticket>(dataReader);
            }

            return tickets;
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            List<Ticket> tickets = new List<Ticket>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Tickets]", connection);

                var dataReader = command.ExecuteReader();

                tickets = QueryHandler.GetList<Ticket>(dataReader);
            }

            return tickets;
        }

        public void DeleteTicket(Int64 ticketId)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"DELETE FROM [Tickets] " +
                    $"WHERE Id = @ticketId", connection);

                command.Parameters.AddWithValue("ticketId", ticketId);

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Ticket> GetTicketsByClass(Int64 flightId, string className)
        {
            List<Ticket> tickets = new List<Ticket>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM Tickets WHERE " +
                    $"Class = @className AND FlightId = @flightId AND OrderId > 0", connection);
                
                command.Parameters.AddWithValue("className", className);
                command.Parameters.AddWithValue("flightId", flightId);

                var dataReader = command.ExecuteReader();

                tickets = QueryHandler.GetList<Ticket>(dataReader);
            }

            return tickets;
        }

        public void UpdateTicket(Ticket ticket)
        {
            Console.WriteLine(ticket.OrderId);
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"UPDATE Tickets SET OrderId = @OrderId WHERE Id = @Id", connection);
                
                command.Parameters.AddWithValue("Id", ticket.Id);
                command.Parameters.AddWithValue("OrderId", ticket.OrderId);

                var dataReader = command.ExecuteReader();

                QueryHandler.GetList<Ticket>(dataReader);
            }
        }

        public void InsertTicket(Ticket ticket)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"INSERT INTO Tickets (FlightId, OrderId, Row, Place, Class, Refund, Price) " +
                    $"VALUES (@FlightId, @OrderId, @Row, @Place, @Class, @Refund, @Price)", connection);
                
                command.Parameters.AddWithValue("FlightId", ticket.FlightId);
                command.Parameters.AddWithValue("OrderId", ticket.OrderId);
                command.Parameters.AddWithValue("Row", ticket.Row);
                command.Parameters.AddWithValue("Place", ticket.Place);
                command.Parameters.AddWithValue("Class", ticket.Class);
                command.Parameters.AddWithValue("Refund", ticket.Refund);
                command.Parameters.AddWithValue("Price", ticket.Price);

                command.ExecuteNonQuery();
            }
        }
    }
}
