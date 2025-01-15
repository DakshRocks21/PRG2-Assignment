using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266136_PRG2Assignment
{
    class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }

        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }
        // CFFT Request Fee
        public override double CalculateFees()
        {
            return RequestFee = 150;
        }
        public override string ToString()
        {
            return base.ToString() + $"\nRequest Fee: {RequestFee}";
        }

    }
    
}
