﻿using Azure;
using BlitzFlug.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BlitzFlug.Repositories
{
    public class OrderRepository : IOrderRepository<Order>
    {
        private string _connectionString;
        private string _connectionInfo;
        private string _userInfo;

        public OrderRepository()
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

        public IEnumerable<Order> GetOrderByUserId(Int64 userId)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Orders] where [Orders].UserId = @UserId", connection);
                command.Parameters.AddWithValue("UserId", userId);

                var dataReader = command.ExecuteReader();

                orders = QueryHandler.GetList<Order>(dataReader);
            }

            return orders;
        }

        public Order GetActiveOrderByUserId(Int64 userId)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Orders] where [Orders].UserId = @UserId " +
                    $"AND Status = 'создан'", connection);

                command.Parameters.AddWithValue("UserId", userId);

                var dataReader = command.ExecuteReader();

                orders = QueryHandler.GetList<Order>(dataReader);

                if (orders.Count == 0)
                    return null;
            }

            return orders[0];
        }

        public void DeleteTicketFromOrder(Int64 ticketId)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command1 = new SqlCommand($"DELETE FROM [OrdersTickets] WHERE TicketId = @ticketId", connection);
                SqlCommand command2 = new SqlCommand($"DELETE FROM [TicketsServices] WHERE TicketId = @ticketId", connection);

                command1.Parameters.AddWithValue("ticketId", ticketId);
                command2.Parameters.AddWithValue("ticketId", ticketId);

                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
            }
        }
        public void DeleteOrder(Int64 orderId)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"DELETE FROM [Orders] " +
                    $"WHERE Id = @OrderId", connection);

                command.Parameters.AddWithValue("OrderId", orderId);

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<OrderedTicket> GetTicketsByOrderId(Int64 userId)
        {
            List<OrderedTicket> orders = new List<OrderedTicket>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT t.Id, f.DeparturePoint, f.ArrivalPoint, f.DepartureDate, " +
                    $"f.ArrivalDate, f.DepartureTime, f.ArrivalTime, t.Row, t.Place, t.Class, t.Refund, " +
                    $"t.Price FROM Orders o JOIN OrdersTickets ot ON o.Id = ot.OrderId JOIN Tickets t ON " +
                    $"ot.TicketId = t.Id JOIN Flights f ON t.FlightId = f.Id WHERE o.UserId = @userId AND o.Status = 'создан'",
                    connection);

                command.Parameters.AddWithValue("userId", userId);

                var dataReader = command.ExecuteReader();

                orders = QueryHandler.GetList<OrderedTicket>(dataReader);
            }

            return orders;
        }

        public decimal GetOrderPrice(Int64 orderId)
        {
            decimal sum = 0.0M;

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                using (var cmd = new SqlCommand("dbo.GetOrderPrice", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 30;
                    SqlCommandBuilder.DeriveParameters(cmd);

                    cmd.Parameters["@orderId"].Value = orderId;

                    cmd.ExecuteNonQuery();

                    sum = (decimal)cmd.Parameters["@RETURN_VALUE"].Value;
                }
            }

            return sum;
        }

        public void InsertOrder(Int64 userId)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"INSERT INTO [Orders] (UserId, Status) VALUES " +
                    $"(@UserId, 'создан')", connection);

                command.Parameters.AddWithValue("UserId", userId);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"UPDATE [Orders] SET UserId = @UserId," +
                    $" Status = @Status WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("Id", order.Id);
                command.Parameters.AddWithValue("UserId", order.UserId);
                command.Parameters.AddWithValue("Status", order.Status);

                command.ExecuteNonQuery();
            }
        }
    }
}