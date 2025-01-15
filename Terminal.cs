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

        
        public bool AddAirline(Airline airline)
        {
            return true;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            return true;
        }

        public Airline GetAirlineFromFlight(Flight flight)
        {
            return new Airline();

        }

        public void PrintAirlineFees()
        {
            return;
        }

        public override string ToString()
        {
            return base.ToString();
        }


    }
    }
}
