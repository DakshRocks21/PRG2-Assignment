using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266136_PRG2Assignment
{
    class NORMFlight : Flight
    {

        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status)
        {
        }
        public override double CalculateFees()
        {
            

            // Arriving Flight Terminal Fees
            if (Destination == "Singapore")
                return 500 + 300;
            else
                return 300;

            // Departing Flight Terminal Fees
            if (Origin == "Singapore")
                return 800 + 300;
            else
                return 300;

            

            
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
    
}
