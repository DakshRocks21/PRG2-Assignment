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
            double fees = 0;

           foreach (Flight flight in Flights.Values)
            {
                fees += flight.CalculateFees();
            }


            if (Flights.Count >= 5)
            {
                fees *= 0.97;
            }

            // int/int = int :D
            fees -= (Flights.Count / 3) * 350.0;


            foreach (var flight in Flights.Values)
            {
                if (flight.ExpectedTime.TimeOfDay < new TimeSpan(11, 0, 0) ||
                    flight.ExpectedTime.TimeOfDay > new TimeSpan(21, 0, 0))
                {
                    fees -= 110.0;
                }
            }

            string[] discountedOrigins = { "DXB", "BKK", "NRT" }; 
            foreach (var flight in Flights.Values)
            {
                if (discountedOrigins.Contains(flight.Origin))
                {
                    fees -= 25.0;
                }
            }

            int normFlightCount = Flights.Values.OfType<NORMFlight>().Count();
            fees -= normFlightCount * 50.0;


            return Math.Max(fees, 0); // hopefully the fees dont go into negative
        }

        public override string ToString()
        {
            return $"Airline: {Name} (Code: {Code}) - Flights: {Flights.Count}";
        }
    }
}
