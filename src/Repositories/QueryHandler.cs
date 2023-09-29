using System.Data;

namespace BlitzFlug.Repositories
{
    public class QueryHandler
    {
        public static List<T> GetList<T>(IDataReader reader) where T : class
        {
            List<T> list = new List<T>();

            while (reader.Read())
            {
                var type = typeof(T);
                T obj = (T)Activator.CreateInstance(type);

                foreach (var prop in type.GetProperties())
                {
                    var propType = prop.PropertyType;
                    prop.SetValue(obj, Convert.ChangeType(reader[prop.Name].ToString(), propType));
                }

                list.Add(obj);
            }

            return list;
        }
    }
}
