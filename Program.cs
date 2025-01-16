//==========================================================
// Student Number	: S10266136
// Student Name	: Daksh Thapar
// Partner Name	: Chua Xiang Qi Theresa
//==========================================================


namespace S10266136_PRG2Assignment

{
    class Program
    {
        static void Main(string[] args)
        {
            Terminal terminal = new Terminal();
            terminal.TerminalName = "Changi Airport Terminal 5";

            // Task 1 & 2;
            InitValues(terminal, "airlines.csv", "boardinggates.csv", "flights.csv");

            while (true)
            {
                Console.WriteLine("\n\n\n\n");
                int menuOption = GetInputFromConsole(DisplayMenu, "Please select your option:", 0, 7);

                switch (menuOption)
                {
                    case 0:
                        // Done
                        Console.WriteLine("Goodbye!");
                        return;
                    case 1:
                        // Task 3 - Daksh (Done)
                        ListAllFlights(terminal);
                        break;
                    case 2:
                        break;
                    case 3:
                        // Task 5 - Daksh (Done)
                        AssignBoardingGateToFlight(terminal);
                        break;
                    case 4:
                        // Task 6 - Daksh (Done)
                        CreateNewFlight(terminal, "flights.csv");
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        // Task 9 - Daksh (Done)
                        DisplayFlightsForDay(terminal); 
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


        static void DisplayFlightsForDay(Terminal terminal)
        {


            List<Flight> flightList = new List<Flight>(terminal.Flights.Values);
            flightList.Sort();

            Console.WriteLine("=============================================");
            Console.WriteLine($"Flight Schedule for {terminal.TerminalName}");
            Console.WriteLine("=============================================");
            Console.WriteLine("Flight Number \t Airline Name \t\t Origin \t\t Destination \t\t Expected Departure/Arrival Time \t Status \t Boarding Gate");


            foreach (var flight in terminal.Flights.Values)
            {
                Airline airline = terminal.GetAirlineFromFlight(flight);
                string boardingGateForFlight = null;
                foreach (KeyValuePair<string, BoardingGate> kvp in terminal.BoardingGates)
                {
                    BoardingGate boardingGate = kvp.Value;
                    if (boardingGate.Flight != null && boardingGate.Flight.FlightNumber == flight.FlightNumber)
                    {
                        boardingGateForFlight = boardingGate.GateName;
                        break;
                    }
                    else
                    {
                        boardingGateForFlight = "Unassigned";
                    }
                }
                Console.WriteLine($"{flight.FlightNumber,-16} {airline.Name,-23} {flight.Origin,-23} {flight.Destination,-23} {flight.ExpectedTime, -39} {flight.Status, -16}{boardingGateForFlight}");
            }
        }

        static void CreateNewFlight(Terminal terminal, string flightsFilePath)
        {
            while (true)
            {
                Console.WriteLine("=============================================");
                Console.WriteLine("Create a New Flight");
                Console.WriteLine("=============================================");


                Console.WriteLine("Enter Flight Number:");
                string flightNumber = Console.ReadLine()?.Trim();


                if (!flightNumber.Contains(" ") || flightNumber.Split(' ')[0].Length != 2)
                {
                    Console.WriteLine("Error: Flight should be in the format (ID XXXX), where ID is the airlines and XXX is the flight number");
                    Console.WriteLine("\n\n\n");
                    continue;
                }

                if (terminal.Flights.ContainsKey(flightNumber))
                {
                    Console.WriteLine($"Error: Flight Number '{flightNumber}' already exists. Cannot add duplicate flights.");
                    Console.WriteLine("\n\n\n");
                    continue;
                }

                try
                {
                    string test = terminal.Airlines[flightNumber.Split(' ')[0]].ToString();
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine($"Error: Airline {flightNumber.Split(' ')[0]} not found! ");
                    Console.WriteLine("\n\n\n");
                    continue;
                }


                Console.WriteLine("Enter Origin:");
                string origin = Console.ReadLine()?.Trim();

                Console.WriteLine("Enter Destination:");
                string destination = Console.ReadLine()?.Trim();

                DateTime expectedTime;
                while (true)
                {
                    Console.WriteLine("Enter Expected Departure/Arrival Time (hh:mm tt) (04:00 pm):");
                    string timeInput = Console.ReadLine()?.Trim();
                    if (DateTime.TryParseExact(timeInput, "hh:mm tt", null, System.Globalization.DateTimeStyles.None, out expectedTime))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date and time format. Please try again.");
                    }
                }

                Console.WriteLine("Would you like to enter a Special Request Code? (Y/N):");
                string addSpecialRequest = Console.ReadLine()?.Trim().ToUpper();

                int specialRequestCode = 0;

                if (addSpecialRequest == "Y")
                {
                    specialRequestCode = GetInputFromConsole(() =>
                    {
                        Console.WriteLine("""
                            Enter Special Request Code:
                            1. LWTT
                            2. DDJB
                            3. CFFT
                            """);
                    }, "Select a code (1-3):", 1, 3);
                }

                Flight flight = specialRequestCode switch
                {
                    1 => new LWTTFlight(flightNumber, origin, destination, expectedTime, "None", 500),
                    2 => new DDJBFlight(flightNumber, origin, destination, expectedTime, "None", 300),
                    3 => new CFFTFlight(flightNumber, origin, destination, expectedTime, "None", 150),
                    _ => new NORMFlight(flightNumber, origin, destination, expectedTime, "None")
                };

                terminal.Flights.Add(flightNumber, flight);
                terminal.Airlines[flightNumber.Split(' ')[0]].AddFlight(flight);

                try
                {
                    using (StreamWriter sw = File.AppendText(flightsFilePath))
                    {
                        string newFlightEntry = $"{flightNumber},{origin},{destination},{expectedTime:hh:mm tt}," +
                            $"{(specialRequestCode == 0 ? "" : specialRequestCode == 1 ? "LWTT" : specialRequestCode == 2 ? "DDJB" : "CFFT")}";
                        sw.WriteLine(newFlightEntry);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing to file: {ex.Message}");
                }


                Console.WriteLine($"Flight {flightNumber} has been successfully added!");

                Console.WriteLine("Would you like to add another flight? (Y/N):");
                string addAnotherFlight = Console.ReadLine()?.Trim().ToUpper();
                if (addAnotherFlight != "Y")
                {
                    break;
                }
            }
        }


        static void AssignBoardingGateToFlight(Terminal terminal)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Assign a Boarding Gate to a Flight");
            Console.WriteLine("=============================================");

            string flightNumber;
            while (true)
            {
                Console.WriteLine("Enter Flight Number:");
                flightNumber = Console.ReadLine();
                if (!terminal.Flights.ContainsKey(flightNumber))
                {
                    Console.WriteLine($"Error: Flight Number '{flightNumber}' does not exist. Please try again.");
                }
                else
                {
                    break;
                }
            }

            string boardingGate;
            while (true)
            {
                Console.WriteLine("Enter Boarding Gate Name:");
                boardingGate = Console.ReadLine();

                if (!terminal.BoardingGates.ContainsKey(boardingGate))
                {
                    Console.WriteLine($"Error: Boarding Gate '{boardingGate}' does not exist. Please try again.");
                }
                else if (terminal.BoardingGates[boardingGate].Flight != null)
                {
                    Console.WriteLine($"Error: Boarding Gate '{boardingGate}' is already assigned to Flight '{terminal.BoardingGates[boardingGate].Flight.FlightNumber}'. Please select a different gate.");
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine(terminal.Flights[flightNumber].ToString());
            string reqcode = terminal.Flights[flightNumber].GetType().Name.Substring(0, 4);
            Console.WriteLine($"Special Request Code: {(reqcode == "NORM" ? "None" :  reqcode)}");
            Console.WriteLine(terminal.BoardingGates[boardingGate].ToString());

            Console.WriteLine("\nWould you like to update the status of the flight? (Y/N):");
            string updateStatus = Console.ReadLine()?.Trim().ToUpper();

            if (updateStatus == "Y")
            {
                int status = GetInputFromConsole(() =>
                {
                    Console.WriteLine("""
                        1. Delayed
                        2. Boarding
                        3. On Time
                        Please select the new status of the flight:
                        """);
                }, "Select flight status:", 1, 3);

                switch (status)
                {
                    case 1:
                        terminal.Flights[flightNumber].Status = "Delayed";
                        break;
                    case 2:
                        terminal.Flights[flightNumber].Status = "Boarding";
                        break;
                    case 3:
                        terminal.Flights[flightNumber].Status = "On Time";
                        break;
                }
            }
            else
            {
                terminal.Flights[flightNumber].Status = "On Time";
            }


            terminal.BoardingGates[boardingGate].Flight = terminal.Flights[flightNumber];
            Console.WriteLine($"Flight {flightNumber} has been successfully assigned to Boarding Gate {boardingGate}!");
        }


        static void InitValues(Terminal terminal, string airlinesFilePath, string boardingGatesFilePath, string flightsFilePath)
        {
            // Task 1 - Theresa (Done)
            LoadAirlines(terminal, airlinesFilePath);
            LoadBoardingGates(terminal, boardingGatesFilePath);

            // Task 2 - Daksh (Done)
            LoadFlights(terminal, flightsFilePath);
        }
        static int GetInputFromConsole(Action displayAction, string prompt, int lowerBound, int upperBound)
        {
            /*
             * SUPPORTS NUMBERS ONLY!
             */
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
        static void LoadAirlines(Terminal terminal, string filePath)
        {
            Console.WriteLine("Loading Airlines...");
            string[] airlineLines = File.ReadAllLines(filePath);
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
            Console.WriteLine($"{airlineLines.Length - 1} Airlines Loaded!");
        }
        static void LoadBoardingGates(Terminal terminal, string filePath)
        {
            Console.WriteLine("Loading Boarding Gates...");
            string[] boardingGateLines = File.ReadAllLines(filePath);
            for (int i = 1; i < boardingGateLines.Length; i++)
            {
                string line = boardingGateLines[i];
                string[] parts = line.Split(',');
                BoardingGate boardingGate = new BoardingGate(parts[0], Convert.ToBoolean(parts[1]), Convert.ToBoolean(parts[2]), Convert.ToBoolean(parts[3]));
                terminal.BoardingGates.Add(parts[0], boardingGate);
            }
            Console.WriteLine($"{boardingGateLines.Length - 1} Boarding Gates Loaded!");
        }

        static void LoadFlights(Terminal terminal, string filePath)
        {
            Console.WriteLine("Loading Flights...");
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');

                    string flightNumber = parts[0].Trim();
                    string origin = parts[1].Trim();
                    string destination = parts[2].Trim();
                    string timeString = parts[3].Trim();
                    string status = (parts.Length == 5) ? parts[4].Trim() : string.Empty;

                    DateTime expectedTime = DateTime.Parse(timeString);

                    Flight flight = null;


                    switch (status.ToUpper())
                    {
                        case "LWTT":
                            flight = new LWTTFlight(flightNumber, origin, destination, expectedTime, "None", 500);
                            break;

                        case "DDJB":
                            flight = new DDJBFlight(flightNumber, origin, destination, expectedTime, "None", 300);
                            break;

                        case "CFFT":
                            flight = new CFFTFlight(flightNumber, origin, destination, expectedTime, "None", 150);
                            break;

                        default:
                            flight = new NORMFlight(flightNumber, origin, destination, expectedTime, "None");
                            break;
                    }

                    if (!terminal.Flights.ContainsKey(flightNumber))
                    {
                        terminal.Flights.Add(flightNumber, flight);
                    }
                    else
                    {
                        terminal.Flights[flightNumber] = flight;
                    }

                    terminal.Airlines[flightNumber.Split(' ')[0]].Flights.Add(flightNumber, flight);
                }
                Console.WriteLine($"{lines.Length - 1} Flights Loaded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading flights: {ex.Message}");
            }
        }

        static void ListAllFlights(Terminal terminal)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine($"List of Flights for {terminal.TerminalName}");
            Console.WriteLine("=============================================");
            Console.WriteLine("Flight Number \t Airline Name \t\t Origin \t\t Destination \t\t Expected Departure/Arrival Time");


            foreach (var flight in terminal.Flights.Values)
            {
                Airline airline = terminal.GetAirlineFromFlight(flight);

                Console.WriteLine($"{flight.FlightNumber,-16} {airline.Name,-23} {flight.Origin,-23} {flight.Destination,-23} {flight.ExpectedTime}");
            }
        }

    }
}

