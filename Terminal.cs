﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number	: S10266136
// Student Name	: Daksh Thapar
// Partner Name	: Chua Xiang Qi Theresa
//==========================================================

namespace S10266136_PRG2Assignment
{
    class Terminal
    {
        public string TerminalName { get; set; } = "Default Terminal";
        public Dictionary<string, Airline> Airlines { get; set; }

        public Dictionary<string, Flight> Flights { get; set; }

        public Dictionary<string, BoardingGate> BoardingGates { get; set; }

        public Dictionary<string, Double> GateFee { get; set; }

        public Terminal()
        {
            Airlines = new Dictionary<string, Airline>();
            Flights = new Dictionary<string, Flight>();
            BoardingGates = new Dictionary<string, BoardingGate>();
            GateFee = new Dictionary<string, Double>();
        }


        public bool AddAirline(Airline airline)
        {
            if (airline == null || Airlines.ContainsKey(airline.Code))
            { return false; }

            Airlines.Add(airline.Code, airline);
            return true;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (boardingGate == null || BoardingGates.ContainsKey(boardingGate.GateName))
            {
                return false;
            }

            BoardingGates.Add(boardingGate.GateName, boardingGate);
            return true;
        }

        public Airline GetAirlineFromFlight(Flight flight)
        {
            if (flight == null || !Flights.ContainsKey(flight.FlightNumber))
            {
                return null; // Flight not found
            }
            foreach (var airline in Airlines.Values)
            {
                if (airline.Flights.ContainsKey(flight.FlightNumber))
                {
                    return airline;
                }
            }

            return null;
        }

        public void PrintAirlineFees()
        {
            Console.WriteLine("Airline Fees:");
            foreach (var airline in Airlines.Values)
            {
                double fee = airline.CalculateFees();
                Console.WriteLine($"{airline.Name}: ${fee:F2}");
            }
        }


        public override string ToString()
        {
            string description = $"Terminal Name: {TerminalName}\n";
            description += $"Number of Airlines: {Airlines.Count}\n";
            description += $"Number of Flights: {Flights.Count}\n";
            description += $"Number of Boarding Gates: {BoardingGates.Count}\n";
            return description;
        }

    }
}