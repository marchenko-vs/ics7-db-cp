﻿using BlitzFlug.Data;
using BlitzFlug.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzFlug.Models
{
    public class OrderedTicket
    {
        public Int64 Id { get; set; }
        public Int64 PlaneId { get; set; }
        public string DeparturePoint { get; set; }
        public string ArrivalPoint { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public Int64 FlightId { get; set; }
        public Int64 OrderId { get; set; }
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
