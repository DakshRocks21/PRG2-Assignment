//==========================================================
// Student Number	: S10266136
// Student Name	: Daksh Thapar
// Partner Name	: Chua Xiang Qi Theresa
//==========================================================

using System.Globalization;
using System.Text.RegularExpressions;

namespace S10266136_PRG2Assignment

{
    class Program
    {
        static void Main(string[] args)
        {

            TerminalManager terminalManager = new TerminalManager();

            terminalManager.AddTerminal("Changi Airport Terminal 5", "datasets\\airlines.csv", "datasets\\boardinggates.csv", "datasets\\flights.csv");
            terminalManager.AddTerminal("Changi Airport Terminal 4", "datasets\\airlines_T4.csv", "datasets\\boardinggates.csv", "datasets\\flights_T4.csv");
            terminalManager.AddTerminal("Changi Airport Terminal 3", "datasets\\airlines_T3.csv", "datasets\\boardinggates.csv", "datasets\\flights_T3.csv");
            terminalManager.AddTerminal("Changi Airport Terminal 2", "datasets\\airlines_T2.csv", "datasets\\boardinggates.csv", "datasets\\flights_T2.csv");
            terminalManager.AddTerminal("Changi Airport Terminal 1", "datasets\\airlines_T1.csv", "datasets\\boardinggates.csv", "datasets\\flights_T1.csv");

            while (true)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("=============================================");
                Console.WriteLine("Welcome to Changi Airport Terminal Manager");
                Console.WriteLine("=============================================");
                Console.WriteLine("1. Select Terminal");
                Console.WriteLine("2. Add New Terminal");
                Console.WriteLine("0. Exit");
                Console.WriteLine("=============================================");

                int choice = GetInputFromConsole(() => { }, "Please select an option:", 0, 2);

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Goodbye!");
                        return;

                    case 1:
                        ListTerminals(terminalManager);
                        break;

                    case 2:
                        Console.Write("Enter New Terminal Name: ");
                        string newTerminalName = Console.ReadLine()?.Trim();

                        Console.Write("Enter Airlines File Path: ");
                        string airlinesFile = Console.ReadLine()?.Trim();

                        Console.Write("Enter Boarding Gates File Path: ");
                        string gatesFile = Console.ReadLine()?.Trim();

                        Console.Write("Enter Flights File Path: ");
                        string flightsFile = Console.ReadLine()?.Trim();

                        terminalManager.AddTerminal(newTerminalName, airlinesFile, gatesFile, flightsFile);
                        Console.WriteLine($"Terminal '{newTerminalName}' has been added!");
                        break;
                }
            }
        }

        static void ListTerminals(TerminalManager terminalManager)
        {
            /*
             * Written by Daksh
             * Bonus Task
             */
            Console.WriteLine("\nAvailable Terminals:");
            var terminals = terminalManager.Terminals.Keys.ToList();

            for (int i = 0; i < terminals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {terminals[i]}");
            }
            Console.WriteLine("0. Return to Main Menu");

            int terminalChoice = GetInputFromConsole(() => { }, "Select a terminal by entering its number:", 0, terminals.Count);

            if (terminalChoice == 0)
            {
                return;
            }

            string selectedTerminalName = terminals[terminalChoice - 1];
            var selectedTerminal = terminalManager.GetTerminal(selectedTerminalName);

            if (selectedTerminal != null)
            {
                ManageTerminal(selectedTerminal);
            }
            else
            {
                Console.WriteLine($"Error: Terminal '{selectedTerminalName}' does not exist.");
            }
        }


        static void ManageTerminal(Terminal terminal)
        {
            while (true)
            {
                Console.WriteLine("\n\n\n\n");
                int menuOption = GetInputFromConsole(() => DisplayMenu(terminal), "Please select your option:", 0, 9);

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
                        //Task 2 - Theresa (Done)
                        DisplayBoardingGates(terminal);
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
                        //Task 7 - Theresa (Done)
                        DisplayAirlineFlights(terminal);
                        break;
                    case 6:
                        //Task 8 - Theresa (Done)
                        ModifyFlightDetails(terminal);
                        break;
                    case 7:
                        // Task 9 - Daksh (Done)
                        DisplayFlightsForDay(terminal);
                        break;
                    case 8:
                        // Advanced Feature 1 - Theresa (Done)
                        ProcessBoardingAssignments(terminal);
                        break;                        
                    case 9:
                        // Advanced Feature 2 - Daksh (Done)
                        DisplayBillForEachAirline(terminal);
                        break;
                }

            }
        }

        static void DisplayMenu(Terminal terminal)
        {
            Console.WriteLine($"""
        =============================================
        Welcome to {terminal.TerminalName}
        =============================================
        1. List All Flights
        2. List Boarding Gates
        3. Assign a Boarding Gate to a Flight
        4. Create Flight
        5. Display Airline Flights
        6. Modify Flight Details
        7. Display Flight Schedule
        8. Process Boarding Assignments
        9. Calculate bill for Each Airline
        0. Exit 

        """);
        }

        public static void InitValues(Terminal terminal, string airlinesFilePath, string boardingGatesFilePath, string flightsFilePath)
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
             * SUPPORTS NUMBERS ONLY
             * Written by Daksh!
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
            /*
             * Written by theresa
             */
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
            /*
             * Written by theresa
             */
            Console.WriteLine("Loading Boarding Gates...");
            string[] boardingGateLines = File.ReadAllLines(filePath);
            for (int i = 1; i < boardingGateLines.Length; i++)
            {
                string line = boardingGateLines[i];
                string[] parts = line.Split(',');
                BoardingGate boardingGate = new BoardingGate(parts[0], Convert.ToBoolean(parts[1]), Convert.ToBoolean(parts[2]), Convert.ToBoolean(parts[3]));
                //terminal.BoardingGates.Add(parts[0], boardingGate);
                terminal.AddBoardingGate(boardingGate);
            }
            Console.WriteLine($"{boardingGateLines.Length - 1} Boarding Gates Loaded!");
        }

        static void DisplayBoardingGates(Terminal terminal)
        {
            /*
             * Written by Theresa
             */
            Console.WriteLine("=============================================");
            Console.WriteLine($"List of Boarding Gates for {terminal.TerminalName}");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Gate Name",-15} {"DDJB",-22} {"CFFT",-22} {"LWTT"}");

            foreach (var boardingGate in terminal.BoardingGates.Values)
            {
                Console.WriteLine($"{boardingGate.GateName,-15} {boardingGate.SupportsDDJB,-22} {boardingGate.SupportsCFFT,-22} {boardingGate.SupportsLWTT}");
            }
        }

        static void LoadFlights(Terminal terminal, string filePath)
        {
            /*
             * Written by Daksh
             */
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
                            flight = new LWTTFlight(flightNumber, origin, destination, expectedTime, "Scheduled", 500);
                            break;

                        case "DDJB":
                            flight = new DDJBFlight(flightNumber, origin, destination, expectedTime, "Scheduled", 300);
                            break;

                        case "CFFT":
                            flight = new CFFTFlight(flightNumber, origin, destination, expectedTime, "Scheduled", 150);
                            break;

                        default:
                            flight = new NORMFlight(flightNumber, origin, destination, expectedTime, "Scheduled");
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
            /*
             * Written by Daksh
             */
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

        static void DisplayFlightsForDay(Terminal terminal)
        {
            /*
             * Written by Daksh
             */
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
                Console.WriteLine($"{flight.FlightNumber,-16} {airline.Name,-23} {flight.Origin,-23} {flight.Destination,-23} {flight.ExpectedTime,-39} {flight.Status,-16}{boardingGateForFlight}");
            }
        }

        static void CreateNewFlight(Terminal terminal, string flightsFilePath)
        {
            /*
             * Written by Daksh
             */
            while (true)
            {
                Console.Write("Enter Flight Number: ");
                string flightNumber = Console.ReadLine()?.Trim();


                if (!Regex.IsMatch(flightNumber, @"^[A-Z]{2} \d{3,4}$"))
                {
                    Console.WriteLine("Error: Flight should be in the format (ID XXXX), where ID is the airlines and X are numbers");
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

                string origin;
                string destination;
                while (true)
                {
                    Console.Write("Enter Origin: ");
                    origin = Console.ReadLine()?.Trim();

                    if (!Regex.IsMatch(origin, @"^[A-Za-z\s]+ \([A-Z]{3}\)$"))
                    {
                        Console.WriteLine("Error: Origin should be in the format 'City (IATA)', e.g., 'Singapore (SIN)'.");
                        continue;
                    }

                    Console.Write("Enter Destination: ");
                    destination = Console.ReadLine()?.Trim();

                    if (!Regex.IsMatch(destination, @"^[A-Za-z\s]+ \([A-Z]{3}\)$"))
                    {
                        Console.WriteLine("Error: Destination should be in the format 'City (IATA)', e.g., 'Sydney (SYD)'.");
                        continue;
                    }

                    if (origin.ToLower() == destination.ToLower())
                    {
                        Console.WriteLine("Error: Both the origin and destination cannot be the same!. Try again!");
                    }
                    else
                    {
                        break;
                    }
                }
                

                DateTime expectedTime;
                while (true)
                {
                    Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                    string timeInput = Console.ReadLine()?.Trim();
                    if (DateTime.TryParseExact(timeInput, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out expectedTime))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid date and time format. Please try again.");
                    }
                }


                Flight flight = null;
                string code = null;

                while (true)
                {
                    Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
                    code = Console.ReadLine();
                    string[] options = { "CFFT", "DDJB", "LWTT", "None" };

                    if (Array.Exists(options, option => option.Equals(code, StringComparison.OrdinalIgnoreCase)))
                    {
                        code = code.ToUpper();
                        switch (code)
                        {
                            case "LWTT":
                                flight = new LWTTFlight(flightNumber, origin, destination, expectedTime, "Scheduled", 500);
                                break;
                            case "DDJB": new DDJBFlight(flightNumber, origin, destination, expectedTime, "Scheduled", 300);
                                 break;
                            case "CFFT":
                                new CFFTFlight(flightNumber, origin, destination, expectedTime, "Scheduled", 150);
                                break;
                            case "NONE":
                                new NORMFlight(flightNumber, origin, destination, expectedTime, "Scheduled");
                                break;

                            default:
                                Console.WriteLine("Error: Code doesnt exist. Try again!");
                                break;
                            
                        };

                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid code entered. Please enter one of the following: CFFT, DDJB, LWTT, or None.");
                    }
                }
        


                terminal.Flights.Add(flightNumber, flight);
                terminal.Airlines[flightNumber.Split(' ')[0]].AddFlight(flight);

                try
                {
                    using (StreamWriter sw = File.AppendText(flightsFilePath))
                    {
                        string newFlightEntry = $"{flightNumber},{origin},{destination},{expectedTime}," + $"{(addSpecialRequest == "Y" ? (code != "None" ? code : "") : "")}";
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

        static void DisplayAirlineFlights(Terminal terminal)
        {
            /*
             * Written by Theresa
             */
            Console.WriteLine($"""
            =============================================
            List of Airlines for {terminal.TerminalName}
            =============================================            
            Airline Code    Airline Name
            """);
            foreach (Airline air in terminal.Airlines.Values)
            {
                Console.WriteLine($"{air.Code,-16}{air.Name}");
            }

            string airlineCode;

            while (true)
            {
                Console.Write("Enter Airline Code: ");
                airlineCode = Console.ReadLine()?.Trim().ToUpper();

                if (terminal.Airlines.ContainsKey(airlineCode))
                {
                    break;
                }

                Console.WriteLine($"Airline '{airlineCode}' does not exist. Please try again !");
            }

            Airline airline = terminal.Airlines[airlineCode];

            Console.WriteLine("=============================================");
            Console.WriteLine($"List of Airlines for {airline.Name}");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-22} {"Origin",-22} {"Destination",-22} {"Expected Departure/Arrival Time"}");

            foreach (var flight in airline.Flights.Values)
            {
                Console.WriteLine($"{flight.FlightNumber,-15} {airline.Name,-22} {flight.Origin,-22} {flight.Destination,-22} {flight.ExpectedTime}");
            }


            string flightNumber;
            while (true)
            {
                Console.Write("Enter a Flight Number: ");
                flightNumber = Console.ReadLine()?.Trim().ToUpper();

                if (airline.Flights.ContainsKey(flightNumber))
                {
                    break;
                }

                Console.WriteLine($"Flight '{flightNumber}' does not exist. Please try again !");
            }

            Console.WriteLine("=============================================");
            Console.WriteLine($"Details for Flight Number {flightNumber}");
            Console.WriteLine("=============================================");

            Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-22} {"Origin",-22} {"Destination",-22} {"Expected Departure/Arrival Time",-35} {"Special Request Code",-25} {"Boarding Gate"}");

            foreach (var flight in airline.Flights.Values)
            {

                if (flight.FlightNumber == flightNumber)
                {
                    string boardingGateName = "Unassigned";
                    foreach (var gate in terminal.BoardingGates.Values)
                    {
                        if (gate.Flight != null && gate.Flight.FlightNumber == flightNumber)
                        {
                            boardingGateName = gate.GateName;
                            break;
                        }
                    }

                    string specialRequestCode = terminal.Flights[flight.FlightNumber].GetType().Name.Substring(0, 4);

                    // If special code is norm, set it to "None"
                    if (string.IsNullOrEmpty(specialRequestCode) || specialRequestCode == "NORM")
                    {
                        specialRequestCode = "None";
                    }

                    Console.WriteLine($"{flight.FlightNumber,-15} {airline.Name,-22} {flight.Origin,-22} {flight.Destination,-22} {flight.ExpectedTime,-35} {specialRequestCode,-25} {boardingGateName,-20}");
                    break;
                }
            }

        }

        static void ModifyFlightDetails(Terminal terminal)
        {
            /*
             * Written by Theresa 
             */
            Console.WriteLine($"""
            =============================================
            List of Airlines for {terminal.TerminalName}
            =============================================            
            Airline Code    Airline Name
            """);
            foreach (Airline air in terminal.Airlines.Values)
            {
                Console.WriteLine($"{air.Code,-16}{air.Name}");
            }

            string airlineCode;

            while (true)
            {
                Console.Write("Enter Airline Code: ");
                airlineCode = Console.ReadLine()?.Trim().ToUpper();

                if (terminal.Airlines.ContainsKey(airlineCode))
                {
                    break;
                }

                Console.WriteLine($"Airline '{airlineCode}' does not exist. Please try again !");
            }

            Airline airline = terminal.Airlines[airlineCode];

            Console.WriteLine("=============================================");
            Console.WriteLine($"List of Airlines for {airline.Name}");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-22} {"Origin",-22} {"Destination",-22} {"Expected Departure/Arrival Time"}");

            foreach (var flight in airline.Flights.Values)
            {
                Console.WriteLine($"{flight.FlightNumber,-15} {airline.Name,-22} {flight.Origin,-22} {flight.Destination,-22} {flight.ExpectedTime}");
            }

            string flightNumber;
            while (true)
            {
                Console.Write("Choose an existing Flight to modify or delete: ");
                flightNumber = Console.ReadLine()?.Trim().ToUpper();

                if (airline.Flights.ContainsKey(flightNumber))
                {
                    break;
                }

                Console.WriteLine($"Flight '{flightNumber}' does not exist. Please try again !");
            }

            // Prompt user for valid input (1 or 2)
            int choice;
            while (true)
            {
                Console.WriteLine($"""
                1. Modify Flight
                2. Delete Flight
                """);
                Console.WriteLine("Choose an option: ");
                string input = Console.ReadLine()?.Trim();

                if (int.TryParse(input, out choice) && (choice == 1 || choice == 2))
                    break;

                Console.WriteLine("Invalid input. Please enter 1 or 2.");
            }

            Flight selectedFlight = airline.Flights[flightNumber];

            switch (choice)
            {
                case 1:
                    // Modify Flight
                    Console.WriteLine($"""
                    1. Modify Basic Information
                    2. Modify Status
                    3. Modify Special Request Code
                    4. Modify Boarding Gate
                    """);

                    int modifyFlightChoice;
                    while (true)
                    {
                        Console.WriteLine("Choose an option: ");
                        string input = Console.ReadLine()?.Trim();

                        if (int.TryParse(input, out modifyFlightChoice) && modifyFlightChoice >= 1 && modifyFlightChoice <= 4)
                            break;

                        Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                    }

                    switch (modifyFlightChoice)
                    {
                        case 1:
                            // Modify Basic Information
                            string newOrigin;
                            string newDestination;

                            while (true)
                            {
                                Console.Write("Enter new Origin: ");
                                newOrigin = Console.ReadLine()?.Trim();
                                bool originExists = terminal.Flights.Values.Any(f =>
                                    f.Origin.Equals(newOrigin, StringComparison.OrdinalIgnoreCase) ||
                                    f.Destination.Equals(newOrigin, StringComparison.OrdinalIgnoreCase));

                                if (originExists)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please key in an origin such as Singapore (SIN).");
                                }
                            }
                            while (true)
                            {
                                Console.Write("Enter new Destination: ");
                                newDestination = Console.ReadLine()?.Trim();
                                bool destinationExists = terminal.Flights.Values.Any(f =>
                                        f.Origin.Equals(newDestination, StringComparison.OrdinalIgnoreCase) ||
                                        f.Destination.Equals(newDestination, StringComparison.OrdinalIgnoreCase));

                                if (destinationExists)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please key in a destination such as Singapore (SIN).");
                                }
                            }
                            while (true)
                            {
                                Console.Write("Enter New Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
                                string newTime = Console.ReadLine()?.Trim();

                                if (DateTime.TryParseExact(newTime, "d/M/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime newExpectedTime))
                                {
                                    // convert to format: "dd/M/yyyy hh:mm:ss tt"
                                    string formattedTime = newExpectedTime.ToString("d/M/yyyy hh:mm:ss tt");

                                    selectedFlight.Origin = newOrigin;
                                    selectedFlight.Destination = newDestination;
                                    selectedFlight.ExpectedTime = newExpectedTime;

                                    Console.WriteLine("Flight updated!");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid date format. Please use d/M/yyyy HH:mm such as 13/1/2025 16:30.");
                                }
                            }
                            break;

                        case 2:
                            // Modify Status
                            while (true)
                            {
                                Console.Write("Enter New Status: ");
                                string newStatus = Console.ReadLine()?.Trim();

                                // Convert to Title Case (First letter of each word capitalized)
                                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                                newStatus = textInfo.ToTitleCase(newStatus.ToLower());

                                selectedFlight.Status = string.IsNullOrEmpty(newStatus) ? "Scheduled" : newStatus;

                                if (newStatus == "Delayed" || newStatus == "Boarding" || newStatus == "On Time")
                                {
                                    Console.WriteLine("Flight status updated successfully!");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid status. Please enter either 'Delayed', 'Boarding', or 'On Time'.");
                                }
                            }
                            break;

                        case 3:
                            // Modify Special Request Code
                            while (true)
                            {
                                Console.Write("Enter New Special Request Code: ");
                                string newSpecialRequest = Console.ReadLine()?.Trim().ToUpper();

                                // Update the special request code by creating a new flight object with the updated code
                                Flight oldFlight = terminal.Flights[flightNumber];
                                Flight newFlight = newSpecialRequest switch
                                {
                                    "CFFT" => new CFFTFlight(oldFlight.FlightNumber, oldFlight.Origin, oldFlight.Destination, oldFlight.ExpectedTime, oldFlight.Status, 150),
                                    "DDJB" => new DDJBFlight(oldFlight.FlightNumber, oldFlight.Origin, oldFlight.Destination, oldFlight.ExpectedTime, oldFlight.Status, 300),
                                    "LWTT" => new LWTTFlight(oldFlight.FlightNumber, oldFlight.Origin, oldFlight.Destination, oldFlight.ExpectedTime, oldFlight.Status, 500),
                                    _ => new NORMFlight(oldFlight.FlightNumber, oldFlight.Origin, oldFlight.Destination, oldFlight.ExpectedTime, oldFlight.Status)
                                };

                                terminal.Flights[flightNumber] = newFlight;

                                if (newSpecialRequest == "DDJB" || newSpecialRequest == "CFFT" || newSpecialRequest == "LWTT" || newSpecialRequest == "NORM")
                                {
                                    Console.WriteLine("Special Request updated successfully!");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid special request. Please enter either 'NORM', 'DDJB', 'CFFT', or 'LWTT'.");
                                }
                            }
                            break;

                        case 4:
                            // Modify Boarding Gate
                            string newBoardingGate;

                            while (true)
                            {
                                Console.Write("Enter New Boarding Gate: ");
                                newBoardingGate = Console.ReadLine()?.Trim();
                                bool BoardingGateExists = terminal.BoardingGates.Values.Any(f => f.GateName.Equals(newBoardingGate, StringComparison.OrdinalIgnoreCase));

                                if (BoardingGateExists && SetBoardingGate(terminal, airline, flightNumber, newBoardingGate))
                                {
                                    Console.WriteLine("Boarding gate updated successfully!");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please key in a boarding gate such as C20.");
                                }
                            }
                            break;
                    }
                    break;

                case 2:
                    // Delete Flight
                    while (true)
                    {
                        Console.Write($"Are you sure you want to delete flight {flightNumber}? (Y/N): ");
                        string confirm = Console.ReadLine()?.Trim().ToUpper();

                        if (confirm == "Y")
                        {
                            airline.Flights.Remove(flightNumber);
                            Console.WriteLine($"Flight {flightNumber} deleted successfully.");
                            break;
                        }
                        else if (confirm == "N")
                        {
                            Console.WriteLine("Deletion cancelled.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter 'Y' or 'N'.");
                        }
                    }
                    break;
            }

            // Display updated flight details      
            foreach (var flight in airline.Flights.Values)
            {
                if (flight.FlightNumber == flightNumber)
                {
                    Console.WriteLine($"""
                    Flight Number: {flight.FlightNumber}
                    Airline Name: {airline.Name}
                    Origin: {flight.Origin}
                    Destination: {flight.Destination}
                    Expected Departure/Arrival Time: {flight.ExpectedTime}
                    Status: {flight.Status}
                    Special Request Code: {terminal.Flights[flight.FlightNumber].GetType().Name.Substring(0, 4)}
                    Boarding Gate: {GetBoardingGate(terminal, airline, flight.FlightNumber)}
                    """);
                }
            }
        }

        static string GetBoardingGate(Terminal terminal, Airline airline, string flightNumber)
        {
            /*
             * Written by Theresa
             */
            // Default boarding gate if not found
            string boardingGateName = "Unassigned";

            // Search for the flight in the airline's flight list
            foreach (var flight in airline.Flights.Values)
            {
                if (flight.FlightNumber == flightNumber)
                {
                    // Check if the flight has a boarding gate assigned
                    foreach (var gate in terminal.BoardingGates.Values)
                    {
                        if (gate.Flight != null && gate.Flight.FlightNumber == flightNumber)
                        {
                            boardingGateName = gate.GateName; // Retrieve boarding gate name
                            break;
                        }
                    }
                    break; // Exit loop once the flight is found
                }
            }
            // Return the boarding gate name (or "Unassigned" if not found)
            return boardingGateName;
        }

        static bool SetBoardingGate(Terminal terminal, Airline airline, string flightNumber, string newBoardingGate)
        {
            /*
             * Written by Theresa
             */
            // Search for the flight in the airline's flight list
            foreach (var flight in airline.Flights.Values)
            {
                if (flight.FlightNumber == flightNumber)
                {
                    // Check if the flight already has a boarding gate assigned
                    foreach (var gate in terminal.BoardingGates.Values)
                    {
                        if (gate.Flight != null && gate.Flight.FlightNumber == flightNumber)
                        {
                            // If the gate is already assigned, update the gate name
                            gate.GateName = newBoardingGate;
                            return true; // Successfully updated the boarding gate
                        }
                    }

                    // If no gate is assigned, try to assign a new available gate
                    foreach (var gate in terminal.BoardingGates.Values)
                    {
                        if (gate.Flight == null) // If gate is available (no flight assigned)
                        {
                            gate.Flight = flight; // Assign the flight to this gate
                            gate.GateName = newBoardingGate; // Set the new boarding gate name
                            return true; // Successfully set the boarding gate
                        }
                    }

                    // If no available gate is found to assign, return false
                    return false;
                }
            }

            // If the flight was not found in the airline's flights, return false
            return false;
        }


        static void AssignBoardingGateToFlight(Terminal terminal)
        {
            /*
             * Written by Daksh
             */
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
                    foreach (BoardingGate bg in terminal.BoardingGates.Values)
                    {

                        if (bg.Flight == null)
                        {
                            continue;
                        }
                        else if (bg.Flight.FlightNumber == flightNumber)
                        {

                            Console.Write($"This flight has been assigned to Gate {bg.GateName}, do you to assign it to a new gate? (Y/N): ");
                            string change = Console.ReadLine();
                            if (change == "Y")
                            {
                                bg.Flight = null;
                                break;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
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
                    string code = terminal.Flights[flightNumber].GetType().Name.Substring(0, 4);

                    if (code == "CFFT")
                    {
                        if (terminal.BoardingGates[boardingGate].SupportsCFFT != true)
                        {
                            Console.WriteLine($"{boardingGate} does not support the requirement of the flight. {code}");
                            continue;
                        }
                    }

                    if (code == "DDJB")
                    {
                        if (terminal.BoardingGates[boardingGate].SupportsDDJB != true)
                        {
                            Console.WriteLine($"{boardingGate} does not support the requirement of the flight. {code}");
                            continue;
                        }
                    }

                    if (code == "LWTT")
                    {
                        if (terminal.BoardingGates[boardingGate].SupportsLWTT != true)
                        {
                            Console.WriteLine($"{boardingGate} does not support the requirement of the flight. {code}");
                            continue;
                        }
                    }

                    break;
                }
            }

            Console.WriteLine(terminal.Flights[flightNumber].ToString());
            string reqcode = terminal.Flights[flightNumber].GetType().Name.Substring(0, 4);
            Console.WriteLine($"Special Request Code: {(reqcode == "NORM" ? "None" : reqcode)}");
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

        static void DisplayBillForEachAirline(Terminal terminal)
        {
            /*
             * Written by Daksh
             */
            int totalNumberOfFlights = terminal.Flights.Count();
            int totalNumberOfFlightsWithGates = 0;

            foreach (BoardingGate boardingGate in terminal.BoardingGates.Values)
            {
                if (boardingGate.Flight != null)
                {
                    totalNumberOfFlightsWithGates += 1;
                }
            }

            if (totalNumberOfFlightsWithGates != totalNumberOfFlights)
            {
                Console.WriteLine("Ensure that all unassigned Flights have their Boarding Gates assigned before running this again");
                return;
            }

            double totalFees = 0;
            double totalDiscounts = 0;

            Console.WriteLine($"{"Airlines",-25} {"Sub Total", -16} {"Discounts", -16} {"Total", -16}");
            foreach (Airline airline in terminal.Airlines.Values)
            {
                double fees = 0.0;

                double discount = 0.0;

                foreach (Flight flight in airline.Flights.Values)
                {
                    fees += flight.CalculateFees();
                }


                if (airline.Flights.Count >= 5)
                {
                    discount += fees * 0.03;
                }

                // int/int = int :D
                discount += (airline.Flights.Count / 3) * 350.0;


                foreach (var flight in airline.Flights.Values)
                {
                    if (flight.ExpectedTime.TimeOfDay < new TimeSpan(11, 0, 0) ||
                        flight.ExpectedTime.TimeOfDay > new TimeSpan(21, 0, 0))
                    {
                        discount += 110.0;
                    }
                }

                string[] discountedOrigins = { "DXB", "BKK", "NRT" };
                foreach (var flight in airline.Flights.Values)
                {
                    if (discountedOrigins.Contains(flight.Origin))
                    {
                        discount += 25.0;
                    }
                }

                int normFlightCount = airline.Flights.Values.OfType<NORMFlight>().Count();
                discount += normFlightCount * 50.0;

                Console.WriteLine($"{airline.Name,-25} {fees,-16} {discount,-16} {fees - discount,-16}");

                totalFees += fees;
                totalDiscounts += discount;
            }

            Console.WriteLine(new string('-', 70));

            Console.WriteLine($"{"Total",-25} {totalFees,-16} {totalDiscounts,-16} {totalFees - totalDiscounts,-16}");

        }

        static void ProcessBoardingAssignments(Terminal terminal)
        {
            /*
             * Advanced Feature
             * Written by Theresa
             */
            Console.WriteLine("=============================================");
            Console.WriteLine("Display Unassigned Flights Details");
            Console.WriteLine("=============================================");

            Queue<Flight> flightQueue = new Queue<Flight>();
            List<Flight> newlyAssignedFlights = new List<Flight>();

            List<BoardingGate> availableGates = terminal.BoardingGates.Values.Where(g => g.Flight == null).ToList(); // List of unassigned gates

            int assignedFlights = 0, assignedGates = 0;
            int autoAssignedFlights = 0, autoAssignedGates = 0;

            // flights w/o boarding gates
            foreach (var airline in terminal.Airlines.Values)
            {
                foreach (var flight in airline.Flights.Values)
                {
                    bool hasGateAssigned = terminal.BoardingGates.Values.Any(g => g.Flight == flight);
                    if (!hasGateAssigned) // If no gate assigned, add to queue
                    {
                        flightQueue.Enqueue(flight);
                    }
                }
            }

            int unassignedFlights = flightQueue.Count;
            int availableGateCount = availableGates.Count;

            Console.WriteLine($"Unassigned Flights: {unassignedFlights}");
            Console.WriteLine($"Available Gates: {availableGateCount}");

            // process flights from queue
            while (flightQueue.Count > 0)
            {
                Flight currentFlight = flightQueue.Dequeue();
                BoardingGate assignedGate = null;

                // Step 3: Assign based on special request code
                string specialRequestCode = terminal.Flights[currentFlight.FlightNumber].GetType().Name.Substring(0, 4);
                assignedGate = availableGates.FirstOrDefault(g => SupportsSpecialRequest(g, specialRequestCode));

                // If no special request match, assign any available gate
                if (assignedGate == null)
                {
                    assignedGate = availableGates.FirstOrDefault();
                }

                // Step 4: Assign gate if available
                if (assignedGate != null)
                {
                    assignedGate.Flight = currentFlight;
                    availableGates.Remove(assignedGate);
                    newlyAssignedFlights.Add(currentFlight);

                    assignedFlights++;
                    assignedGates++;
                    autoAssignedFlights++;
                    autoAssignedGates++;
                }
            }

            // Step 5: Display updated flight details
            Console.WriteLine();
            Console.WriteLine("Updated Flight Details:");
            Console.WriteLine();
            Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-22} {"Origin",-22} {"Destination",-22} {"Expected Departure/Arrival Time",-35} {"Special Request Code",-25} {"Boarding Gate",-15}");

            foreach (var flight in newlyAssignedFlights)
            {
                string airlineName = terminal.Airlines.Values.FirstOrDefault(a => a.Flights.ContainsKey(flight.FlightNumber))?.Name ?? "N/A";
                string specialRequestCode = terminal.Flights[flight.FlightNumber].GetType().Name.Substring(0, 4);
                if (string.IsNullOrEmpty(specialRequestCode) || specialRequestCode == "NORM")
                {
                    specialRequestCode = "N/A";
                }
                //string specialRequestCode = specialRequestCodes.ContainsKey(flight.FlightNumber) ? specialRequestCodes[flight.FlightNumber] : "N/A";
                string boardingGate = terminal.BoardingGates.Values.FirstOrDefault(g => g.Flight == flight)?.GateName ?? "Unassigned";
                Console.WriteLine($"{flight.FlightNumber,-15} {airlineName,-22} {flight.Origin,-22} {flight.Destination,-22} {flight.ExpectedTime,-35} {specialRequestCode,-25} {boardingGate,-15}");
            }

            // Step 6: Display statistics
            int remainingUnassignedFlights = flightQueue.Count; // Remaining flights w/o gates
            int remainingUnassignedGates = availableGates.Count; // Remaining unassigned gates

            Console.WriteLine();
            Console.WriteLine("=============================================");
            Console.WriteLine("Processing Summary");
            Console.WriteLine("=============================================");
            Console.WriteLine($"Total Flights Assigned: {assignedFlights}");
            Console.WriteLine($"Total Boarding Gates Assigned: {assignedGates}");
            Console.WriteLine($"Remaining Flights without Boarding Gates: {remainingUnassignedFlights}");
            Console.WriteLine($"Remaining Boarding Gates without Flights: {remainingUnassignedGates}");
            Console.WriteLine($"Percentage of Flights Assigned Automatically: {(unassignedFlights > 0 ? (assignedFlights * 100 / unassignedFlights) : 0)}%");
            Console.WriteLine($"Percentage of Boarding Gates Assigned Automatically: {(availableGateCount > 0 ? (assignedGates * 100 / availableGateCount) : 0)}%");

        }

        static bool SupportsSpecialRequest(BoardingGate gate, string specialRequestCode)
        {
            /*
             * Written by Theresa
             */
            return specialRequestCode switch
            {
                "CFFT" => gate.SupportsCFFT,
                "DDJB" => gate.SupportsDDJB,
                "LWTT" => gate.SupportsLWTT,
                _ => false,
            };
        }

    }
}