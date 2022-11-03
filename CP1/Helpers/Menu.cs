namespace CP1.Helpers;

public class Menu
{
    // attributes
    private static string ExitCode = "exit";
    // methods
    public void Start() 
    {
        RenderMenu();
    }

    public void RenderMenu()
    {
        string Option = "";
        do
        {
            Console.Clear();
            Console.WriteLine("1. Option 1");
            Console.WriteLine("Write \"exit\" to exit");
            Option = Console.ReadLine();
            Console.Clear();
            switch (Option)
            {
                case "1":
                    Console.WriteLine("You chose option 1");
                    break;
                default:
                    break;
            }
            Thread.Sleep(2300);
        }
        while (!Option.Equals(ExitCode));
        Console.WriteLine("You left the program, good bye!");
        Environment.Exit(1);// exit
    }

}
