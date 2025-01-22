//==========================================================
// Student Number	: S10266136
// Student Name	: Daksh Thapar
// Partner Name	: Chua Xiang Qi Theresa
//==========================================================


namespace S10266136_PRG2Assignment
{
    class NORMFlight : Flight
    {

        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status)
        {
        }
        public override double CalculateFees()
        {
            // Base + gate fee
            return base.CalculateFees() + 300.0;
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
    
}
