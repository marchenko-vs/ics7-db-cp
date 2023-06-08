using BlitzFlug.Data;
using BlitzFlug.Models;
using Microsoft.Data.SqlClient;

namespace BlitzFlug.Repositories
{
    public class PlaneRepository : IPlaneRepository<Plane>
    {
        private string _connectionString;
        private string _connectionInfo;
        private string _userInfo;

        public PlaneRepository()
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

        public PlaneRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IEnumerable<Plane> GetAllPlanes()
        {
            List<Plane> planes = new List<Plane>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Planes]", connection);

                var dataReader = command.ExecuteReader();

                planes = QueryHandler.GetList<Plane>(dataReader);
            }

            return planes;
        }

        public Plane GetPlaneById(Int64 planeId)
        {
            List<Plane> planes = new List<Plane>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM [Planes] where Id = @planeId", connection);
                command.Parameters.AddWithValue("planeId", planeId);

                var dataReader = command.ExecuteReader();

                planes = QueryHandler.GetList<Plane>(dataReader);

                if (planes.Count == 0)
                    return null;
            }

            return planes[0];
        }

        public void DeletePlane(Int64 planeId)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"DELETE FROM [Planes] " +
                    $"WHERE Id = @planeId", connection);

                command.Parameters.AddWithValue("planeId", planeId);

                command.ExecuteNonQuery();
            }
        }

        public void InsertPlane(Plane plane)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"INSERT INTO [Planes] (Manufacturer, Model, " +
                    $"EconomyClassNum, FirstClassNum, BusinessClassNum) VALUES " +
                    $"(@Manufacturer, @Model, @EconomyClassNum, @FirstClassNum, @BusinessClassNum)", connection);

                command.Parameters.AddWithValue("Manufacturer", plane.Manufacturer);
                command.Parameters.AddWithValue("Model", plane.Model);
                command.Parameters.AddWithValue("EconomyClassNum", plane.EconomyClassNum);
                command.Parameters.AddWithValue("FirstClassNum", plane.FirstClassNum);
                command.Parameters.AddWithValue("BusinessClassNum", plane.BusinessClassNum);

                command.ExecuteNonQuery();
            }
        }

        public void UpdatePlane(Plane plane)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"UPDATE [Planes] SET Manufacturer = @Manufacturer, " +
                    $"Model = @Model, EconomyClassNum = @EconomyClassNum, FirstClassNum = @FirstClassNum, " +
                    $"BusinessClassNum = @BusinessClassNum WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("Id", plane.Id);
                command.Parameters.AddWithValue("Manufacturer", plane.Manufacturer);
                command.Parameters.AddWithValue("Model", plane.Model);
                command.Parameters.AddWithValue("EconomyClassNum", plane.EconomyClassNum);
                command.Parameters.AddWithValue("FirstClassNum", plane.FirstClassNum);
                command.Parameters.AddWithValue("BusinessClassNum", plane.BusinessClassNum);

                command.ExecuteNonQuery();
            }
        }
    }
}
