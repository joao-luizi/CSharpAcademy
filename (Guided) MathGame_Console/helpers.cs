using CodeAcademy_Console.Models;

namespace CodeAcademy_Console
{
    internal class Helpers
    {
        internal static List<Game> games = new();
        internal static string GetName()
        {
            Console.WriteLine("Please type your name");
            var name = Console.ReadLine();
            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name can't be empty");
                name = Console.ReadLine();
            }
            return name;
        }

        internal static void AddToHistory(int gameScore, GameType gameType)
        {
            games.Add(
                new Game
                {
                    Date = DateTime.Now,
                    Score = gameScore,
                    Type = gameType
                });
        }

        internal static void PrintGames()
        {
            var gamesToPrint = games.Where(x => x.Date > new DateTime(2022, 09, 08)).OrderByDescending(x => x.Score > 3);
            Console.Clear();
            Console.WriteLine("Games History");
            Console.WriteLine("-----------------------------------");
            foreach (var game in gamesToPrint)
            {
                Console.WriteLine($"{game.Date} - {game.Type}: {game.Score} pts");
            }
            Console.WriteLine("-----------------------------------\n");
            Console.WriteLine("Press any key to return to Main Menu");
            Console.ReadLine();
        }

        internal static int[] GetDivisionNubers()
        {
            var random = new Random();
            var firstNumber = random.Next(1, 99);
            var secondNumber = random.Next(1, 99);

            var result = new int[2];
            while (firstNumber % secondNumber != 0)
            {
                firstNumber = random.Next(1, 99);
                secondNumber = random.Next(1, 99);
            }

            result[0] = firstNumber;
            result[1] = secondNumber;

            return result;
        }

        internal static string ValidateInput(string result)
        {

            while (string.IsNullOrEmpty(result) || !Int32.TryParse(result, out _))
            {
                Console.WriteLine("Your answer need to be an Integer. Try again.");
                result = Console.ReadLine();
            }
            return result;
        }

    }
}