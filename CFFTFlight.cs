//==========================================================
// Student Number	: S10266136
// Student Name	: Daksh Thapar
// Partner Name	: Chua Xiang Qi Theresa
//==========================================================


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
            double baseFees = 300; // Boarding Gate Base Fee
            double total = baseFees;
            double requestFee = 150; // Request Fee

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
