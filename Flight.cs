//==========================================================
// Student Number	: S10266136
// Student Name	: Daksh Thapar
// Partner Name	: Chua Xiang Qi Theresa
//==========================================================


namespace S10266136_PRG2Assignment
{
    abstract class Flight : IComparable<Flight>
    {

        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; } = "Scheduled";

        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            this.FlightNumber = flightNumber;
            this.Origin = origin;
            this.Destination = destination;
            this.ExpectedTime = expectedTime;
            this.Status = status;
        }
        public virtual double CalculateFees()
        {
            // I did this because a flight cannot take off and land in singapore!
            if (Destination.StartsWith("Singapore"))
            {
                return 500.0;
            }
            if (Origin.StartsWith("Singapore"))
            {
                return 800.0;
            }

            return 0.0;

        }

        public override string ToString()
        {
            return $"Flight Number: {this.FlightNumber}\nOrigin: {this.Origin}\nDestination: {this.Destination}\nExpected Time: {this.ExpectedTime}\nStatus: {this.Status}";
        }
        public int CompareTo(Flight other)
        {
            if (other == null) return 1;
            return this.ExpectedTime.CompareTo(other.ExpectedTime);
        }
    }
}