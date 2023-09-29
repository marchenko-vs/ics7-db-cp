namespace BlitzFlug.Repositories
{
    public interface IPlaneRepository<T> 
        where T : class
    {
        IEnumerable<T> GetAllPlanes();
        T GetPlaneById(Int64 planeId);
        void InsertPlane(T plane);
        void UpdatePlane(T plane);
        void DeletePlane(Int64 planeId);
    }
}
