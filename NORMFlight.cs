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
            
            // Both Arriving Flight Terminal Fees = 500
            // 300 is the base fee

            double baseFees = 300;
            double total = baseFees;

            if (Destination == "Singapore")
            {
                total += 500;
            }
            else
            {
                total += 800;
            }
            return total;
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
    
}
