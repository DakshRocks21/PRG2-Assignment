//==========================================================
// Student Number	: S10266136
// Student Name	: Daksh Thapar
// Partner Name	: Chua Xiang Qi Theresa
//
// Created by Daksh for his BONUS Task.
//==========================================================


using S10266136_PRG2Assignment;

class TerminalManager
{
    public Dictionary<string, Terminal> Terminals { get; private set; }

    public TerminalManager()
    {
        Terminals = new Dictionary<string, Terminal>();
    }

    public void AddTerminal(string terminalName, string airlinesFile, string gatesFile, string flightsFile)
    {
        var terminal = new Terminal { TerminalName = terminalName };
        Program.InitValues(terminal, airlinesFile, gatesFile, flightsFile);
        Terminals[terminalName] = terminal;
    }

    public Terminal GetTerminal(string terminalName)
    {
        return Terminals.ContainsKey(terminalName) ? Terminals[terminalName] : null;
    }

    public void ListTerminals()
    {
        Console.WriteLine("Available Terminals:");
        foreach (var terminal in Terminals.Keys)
        {
            Console.WriteLine($"- {terminal}");
        }
    }
}
