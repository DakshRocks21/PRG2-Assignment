﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266136_PRG2Assignment
{
    abstract class Flight
    {

        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; } = "None";

        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            this.FlightNumber = flightNumber;
            this.Origin = origin;
            this.Destination = destination;
            this.ExpectedTime = expectedTime;
            this.Status = status;
        }
        public abstract double CalculateFees();

        public override string ToString()
        {
            return $"Flight Number: {this.FlightNumber}\nOrigin: {this.Origin}\nDestination: {this.Destination}\nExpected Time: {this.ExpectedTime}\nSpecial Request Code: {this.Status}";
        }
    }
}