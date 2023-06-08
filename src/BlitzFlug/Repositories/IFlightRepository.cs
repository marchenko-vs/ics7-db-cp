namespace BlitzFlug.Repositories
{
    public interface IFlightRepository<T> 
        where T : class
    {
        public IEnumerable<T> GetAllFlights();
        public IEnumerable<T> GetFlights(String departurePoint, String arrivalPoint, DateTime departureDate);
        public void InsertFlight(T flight);
        public void UpdateFlight(T flight);
        public void DeleteFlight(Int64 flightId);
    }
}
