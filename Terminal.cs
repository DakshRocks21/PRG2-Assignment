using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266136_PRG2Assignment
{
    class Terminal
    {
        public string TerminalName { get; set; }
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

        public void LoadFlights(string filePath)
        {
            Console.WriteLine("Loading Flights...");
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');

                    string flightNumber = parts[0].Trim();
                    string origin = parts[1].Trim();
                    string destination = parts[2].Trim();
                    string timeString = parts[3].Trim();
                    string status = (parts.Length == 5) ? parts[4].Trim() : string.Empty;

                    DateTime expectedTime = DateTime.Parse(timeString);

                    Flight flight = null;


                    switch (status.ToUpper())
                    {
                        case "LWTT":
                            flight = new LWTTFlight(flightNumber, origin, destination, expectedTime, status, 500);
                            break;

                        case "DDJB":
                            flight = new DDJBFlight(flightNumber, origin, destination, expectedTime, status, 300);
                            break;

                        case "CFFT":
                            flight = new CFFTFlight(flightNumber, origin, destination, expectedTime, status, 150);
                            break;

                        default:
                            flight = new NORMFlight(flightNumber, origin, destination, expectedTime, status);
                            break;
                    }

                    if (!Flights.ContainsKey(flightNumber))
                    {
                        Flights.Add(flightNumber, flight);
                    }
                    else
                    {
                        Flights[flightNumber] = flight;
                    }

                    Airlines[flightNumber.Split(' ')[0]].Flights.Add(flightNumber, flight);
                }
                Console.WriteLine($"{lines.Length - 1} Flights Loaded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading flights: {ex.Message}");
            }
        }

        public void ListAllFlights()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine($"List of Flights for {TerminalName}");
            Console.WriteLine("=============================================");
            Console.WriteLine("Flight Number \t Airline Name \t\t Origin \t\t Destination \t\t Expected Departure/Arrival Time");

            foreach (var flight in Flights.Values)
            {
                Airline airline= GetAirlineFromFlight(flight);
                Console.WriteLine($"{flight.FlightNumber,-16} {airline.Name,-23} {flight.Origin,-23} {flight.Destination,-23} {flight.ExpectedTime:dd/MM/yyyy hh:mm:ss tt}");
            }
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