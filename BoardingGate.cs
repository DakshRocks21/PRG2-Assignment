//==========================================================
// Student Number	: S10266136
// Student Name	: Daksh Thapar
// Partner Name	: Chua Xiang Qi Theresa
//==========================================================


namespace S10266136_PRG2Assignment
{
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
        }

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

        public override string ToString()
        {
            return $"""
            Boarding Gate Name: {GateName}
            Supports DDJB: {SupportsDDJB}
            Supports CFFT: {SupportsCFFT}
            Supports LWTT: {SupportsLWTT}
            """;
        }

    }
}