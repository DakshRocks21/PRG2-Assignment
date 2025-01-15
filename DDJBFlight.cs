using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266136_PRG2Assignment
{
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }

        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }

        // DDJB Request Fee
        public override double CalculateFees()
        {
            return RequestFee = 300;
        }
        public override string ToString()
        {
            return base.ToString() + $"\nRequest Fee: {RequestFee}";
        }





    }
}
