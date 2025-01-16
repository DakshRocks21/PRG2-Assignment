using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266136_PRG2Assignment
{
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }

        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }

        //LWTT Request Fee
        public override double CalculateFees()
        {

            double baseFees = 300; // Boarding Gate Base Fee
            double total = baseFees;
            double requestFee = 500; // Request Fee

            if (Destination == "Singapore") 
            {
                total += 500; 
            }
            else 
            {
                total += 800; 
            }

            total += requestFee; // Add special request fee
            return total;
        }
        public override string ToString()
        {
            return base.ToString() + $"\nRequest Fee: {RequestFee}";
        }


    }
}
