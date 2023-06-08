using BlitzFlug.Data;
using BlitzFlug.Models;
using Microsoft.Data.SqlClient;

namespace BlitzFlug.Repositories
{
    public class FlightRepository : IFlightRepository<Flight>
    {
        private string _connectionString;
        private string _connectionInfo;
        private string _userInfo;

        public FlightRepository()
        {
            this._connectionInfo = Startup.ConnectionString;
            var currentUser = SingletonUser.GetInstance();

            if (null == currentUser || null == currentUser.UserInfo)
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

        public IEnumerable<Flight> GetAllFlights()
        {
            List<Flight> flights = new List<Flight>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM Flights", connection);

                var dataReader = command.ExecuteReader();

                flights = QueryHandler.GetList<Flight>(dataReader);
            }

            return flights;
        }

        public void DeleteFlight(Int64 flightId)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"DELETE FROM Flights " +
                    $"WHERE Id = @flightId", connection);

                command.Parameters.AddWithValue("flightId", flightId);

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<Flight> GetFlights(string departurePoint, string arrivalPoint, DateTime departureDate)
        {
            List<Flight> flights = new List<Flight>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM Flights WHERE " +
                    $"DeparturePoint = @departurePoint AND " +
                    $"ArrivalPoint = @arrivalPoint AND " +
                    $"CONVERT(date, DepartureDateTime) = @departureDateTime " +
                    $"ORDER BY DepartureDateTime, ArrivalDateTime", connection);

                command.Parameters.AddWithValue("DeparturePoint", departurePoint);
                command.Parameters.AddWithValue("ArrivalPoint", arrivalPoint);
                command.Parameters.AddWithValue("DepartureDateTime", departureDate);

                var dataReader = command.ExecuteReader();

                flights = QueryHandler.GetList<Flight>(dataReader);
            }

            return flights;
        }

        public void InsertFlight(Flight flight)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"INSERT INTO Flights (PlaneId, DeparturePoint, ArrivalPoint, DepartureDateTime, ArrivalDateTime) " +
                    $"VALUES (@PlaneId, @DeparturePoint, @ArrivalPoint, @DepartureDateTime, @ArrivalDateTime)", connection);

                command.Parameters.AddWithValue("PlaneId", flight.PlaneId);
                command.Parameters.AddWithValue("DeparturePoint", flight.DeparturePoint);
                command.Parameters.AddWithValue("ArrivalPoint", flight.ArrivalPoint);
                command.Parameters.AddWithValue("DepartureDate", flight.DepartureDateTime);
                command.Parameters.AddWithValue("ArrivalDate", flight.ArrivalDateTime);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateFlight(Flight flight)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"UPDATE Flights SET PlaneId = @PlaneId, DeparturePoint = @DeparturePoint, ArrivalPoint = @ArrivalPoint, " +
                    $"DepartureDateTime = @DepartureDate, ArrivalDateTime = @ArrivalDateTime " +
                    $"WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("Id", flight.Id);
                command.Parameters.AddWithValue("PlaneId", flight.PlaneId);
                command.Parameters.AddWithValue("DeparturePoint", flight.DeparturePoint);
                command.Parameters.AddWithValue("ArrivalPoint", flight.ArrivalPoint);
                command.Parameters.AddWithValue("DepartureDateTime", flight.DepartureDateTime);
                command.Parameters.AddWithValue("ArrivalDateTime", flight.ArrivalDateTime);

                command.ExecuteNonQuery();
            }
        }
    }
}
