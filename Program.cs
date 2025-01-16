using System.Buffers.Text;

namespace S10266136_PRG2Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Terminal terminal = new Terminal();
            terminal.TerminalName = "Changi Airport Terminal 5";

            // Task 1.1 - Your Welcome !
            string[] airlineLines = File.ReadAllLines("airlines.csv");

            for (int i = 1; i < airlineLines.Length; i++)
            {
                string line = airlineLines[i];
                string[] parts = line.Split(',');
                Airline airline = new Airline
                {
                    Name = parts[0],
                    Code = parts[1]
                };
                terminal.AddAirline(airline);
            }

            // Task 1 Boarding Gates - Theresa (Done)
            string[] boardingGatesSpecialReq = File.ReadAllLines("boardingGates.csv");

            for (int i = 1;i < boardingGatesSpecialReq.Length; i++)
            {
                string line = boardingGatesSpecialReq[i];
                string[] parts = line.Split(',');
                BoardingGate boardingGate = new BoardingGate
                {
                    GateName = parts[0],
                    SupportsCFFT = parts[1] == "Y",
                    SupportsDDJB = parts[2] == "Y",
                    SupportsLWTT = parts[3] == "Y"
                };
                terminal.AddBoardingGate(boardingGate);
            }


            // Task 2 - Daksh (Done)
            terminal.LoadFlights("flights.csv");


            while (true)
            {
                Console.WriteLine("\n\n\n\n");
                int menuOption = GetInputFromConsole(DisplayMenu, "Please select your option:", 0, 7);

                switch (menuOption)
                {
                    case 0:
                        return;
                    case 1:
                        // Task 3 - Daksh (Done)
                        terminal.ListAllFlights();
                        break;
                    case 2:
                        //terminal.ListBoardingFlights();
                        break;
                    default:
                        Console.WriteLine("Something went wrong");
                        break;
                }

            }
        }


        static void DisplayMenu()
        {
            Console.WriteLine("""
        =============================================
        Welcome to Changi Airport Terminal 5
        =============================================
        1. List All Flights
        2. List Boarding Gates
        3. Assign a Boarding Gate to a Flight
        4. Create Flight
        5. Display Airline Flights
        6. Modify Flight Details
        7. Display Flight Schedule
        0. Exit 

        """);
        }

        static int GetInputFromConsole(Action displayAction, string prompt, int lowerBound, int upperBound)
        {
            int result;
            while (true)
            {
                displayAction();

                Console.WriteLine(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out result) && result >= lowerBound && result <= upperBound)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again. ");
                    Console.WriteLine($"Your input should be between {lowerBound} and {upperBound}");
                }

                Console.WriteLine("\n\n\n\n");
            }
        }

    }
}

