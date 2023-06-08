using BlitzFlug.Models;
using Moq;
using BlitzFlug.Repositories;

namespace BlitzFlugUnitTests
{
    public class FlightTests
    {
        [Fact]
        public void GetFlights()
        {
            string departurePoint = "Москва";
            string arrivalPoint = "Белгород";
            string departureDate = "2023-03-20";
            DateTime date = new DateTime(2023, 03, 20);

            var mock = new Mock<IFlightRepository<Flight>>();

            mock.Setup(repo => repo.GetFlights(departurePoint, arrivalPoint, date)).
                Returns(AvailableFlights(departurePoint, arrivalPoint, date));

            var flight = new Flight(mock.Object);
            List<Flight> flights = flight.GetFlights(departurePoint, arrivalPoint, departureDate).ToList();

            Assert.Equal(4, flights.Count);
        }

        private IEnumerable<Flight> AvailableFlights(string departurePoint, string arrivalPoint, DateTime departureDate)
        {
            return new List<Flight>() {
                new Flight() { Id = 1, DeparturePoint = "Москва", ArrivalPoint = "Белогород", DepartureDate = new DateTime(2023, 03, 20) },
                new Flight() { Id = 2, DeparturePoint = departurePoint, ArrivalPoint = arrivalPoint, DepartureDate = new DateTime(2023, 03, 20) },
                new Flight() { Id = 3, DeparturePoint = departurePoint, ArrivalPoint = arrivalPoint, DepartureDate = new DateTime(2023, 03, 20) },
                new Flight() { Id = 4, DeparturePoint = departurePoint, ArrivalPoint = arrivalPoint, DepartureDate = new DateTime(2023, 03, 20) },
            };
        }
    }
}
