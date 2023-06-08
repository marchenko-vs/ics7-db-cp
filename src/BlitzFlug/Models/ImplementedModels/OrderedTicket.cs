using BlitzFlug.Data;
using BlitzFlug.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzFlug.Models
{
    public class OrderedTicket
    {
        public Int64 Id { get; set; }
        public string DeparturePoint { get; set; }
        public string ArrivalPoint { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int Row { get; set; }
        public char Place { get; set; }
        public string Class { get; set; }
        public bool Refund { get; set; }
        public Decimal Price { get; set; }

        public OrderedTicket()
        {

        }
    }
}
