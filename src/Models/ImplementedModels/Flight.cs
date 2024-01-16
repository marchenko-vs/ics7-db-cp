using BlitzFlug.Data;
using BlitzFlug.Repositories;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlitzFlug.Models
{
    public class Flight
    {
        private IFlightRepository<Flight> _db;

        public Int64 Id { get; set; }
        public Int64 PlaneId { get; set; }
        public string DeparturePoint { get; set; }
        public string ArrivalPoint { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }

        public Flight()
        {
            this.DeparturePoint = string.Empty;
            this.ArrivalPoint = string.Empty;

            var factory = new MsSqlRepositoryFactory();
            this._db = factory.CreateFlightRepository();
        }

        public Flight(IFlightRepository<Flight> db)
        {
            this.DeparturePoint = string.Empty;
            this.ArrivalPoint = string.Empty;

            this._db = db;
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return this._db.GetAllFlights();
        }

        public IEnumerable<string> GetUniquePoints() // arrival and departure points
        {
            SortedSet<string> set = new SortedSet<string>();
            IEnumerable<Flight> flights = this._db.GetAllFlights();

            foreach (var flight in flights)
            {
                set.Add(flight.DeparturePoint);
                set.Add(flight.ArrivalPoint);
            }

            return set.ToList();
        }

        public IEnumerable<Flight> GetFlights()
        {
            return this._db.GetFlights(this.DeparturePoint, this.ArrivalPoint, this.DepartureDateTime);            
        }

        public Flight GetById()
        {
            return this._db.GetFlightById(this.Id);
        }

        public void InsertFlight()
        {
            this._db.InsertFlight(this);
        }

        public void AddFlight()
        {
            this._db.InsertFlight(this);
        }

        public void DeleteFlight()
        {
            this._db.DeleteFlight(this.Id);
        }
    }
}
