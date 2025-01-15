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
            return 0.0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
