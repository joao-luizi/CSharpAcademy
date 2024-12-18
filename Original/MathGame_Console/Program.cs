namespace Branch_Console;

class Program
{
    static void Main(string[] args)
    {
        var context = new Context(new SetName());
        var isGameOn = context.GetGameOn();

        while (isGameOn)
        {
            context.SetUpConsole();
            context.ProcessInput();
            isGameOn = context.GetGameOn();
        }
        Console.Clear();
        Console.WriteLine("Thank you for playing!");
        Console.WriteLine("If you enjoyed consider going to https://github.com/joao-luizi/CSharp-Academy/tree/MathGame-Original-Console and giving it a ⭐ ");



    }
}