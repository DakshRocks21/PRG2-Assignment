//==========================================================
// Student Number	: S10266136
// Student Name	: Daksh Thapar
// Partner Name	: Chua Xiang Qi Theresa
//==========================================================

namespace S10266136_PRG2Assignment
{
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }

        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }

        public override double CalculateFees()
        {
            // Base + gate fee + special fee
            return base.CalculateFees() + 300.0 + 300.0;
        }

        public override string ToString()
        {
            return base.ToString() + $"\nRequest Fee: {RequestFee}";
        }





    }
}
