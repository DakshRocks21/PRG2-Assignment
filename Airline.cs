//==========================================================
// Student Number	: S10266136
// Student Name	: Daksh Thapar
// Partner Name	: Chua Xiang Qi Theresa
//==========================================================

namespace S10266136_PRG2Assignment
{
    class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }

        public Airline()
        {
            Flights = new Dictionary<string, Flight>();
        }


  
        public bool AddFlight(Flight flight)
        {
            if (flight == null || Flights.ContainsKey(flight.FlightNumber))
            {
                return false; 
            }

            Flights.Add(flight.FlightNumber, flight);
            return true;
        }

        public bool RemoveFlight(Flight flight)
        {
            if (flight == null || !Flights.ContainsKey(flight.FlightNumber))
            {
                return false; 
            }

            Flights.Remove(flight.FlightNumber);
            return true;
        }

        public double CalculateFees()
        {
            // TODO: Finish this class
            return 0.0; 
           
        }

        public override string ToString()
        {
            return $"Airline: {Name} (Code: {Code}) - Flights: {Flights.Count}";
        }
    }
}
