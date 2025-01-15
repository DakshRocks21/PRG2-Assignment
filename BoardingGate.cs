using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace S10266136_PRG2Assignment
{
    class BoardingGate
    {
        public string GateName {  get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public double CalculateFees()
        {
            if (Flight == null)
            {
                return 0.0; 
            }

            double baseFee = 100.0; 
             
            // TODO
            if (SupportsCFFT && Flight.Status == "CFFT") baseFee += 50.0;
            if (SupportsDDJB && Flight.Status == "DDJB") baseFee += 40.0;
            if (SupportsLWTT && Flight.Status == "LWTT") baseFee += 30.0;

            return baseFee;
        }
        
        // TODO: Current ToString is for Debugging :D
        public override string ToString()
        {
            string supportInfo = $"Supports: "
                + (SupportsCFFT ? "CFFT " : "")
                + (SupportsDDJB ? "DDJB " : "")
                + (SupportsLWTT ? "LWTT " : "");
            string flightInfo = Flight != null ? $"Assigned Flight: {Flight.FlightNumber}" : "No flight assigned";
            return $"{GateName} - {supportInfo}- {flightInfo}";
        }
    }
}
