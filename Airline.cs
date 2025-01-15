using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266136_PRG2Assignment
{
    class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }

        public bool AddFlight(Flight flight)
        {
            return true;
        }

        public bool RemoveFlight(Flight flight)
        {
            return false;
        }

        public double CalculateFees()
        {
            return 0.0;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
