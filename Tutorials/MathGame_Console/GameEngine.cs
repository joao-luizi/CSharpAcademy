
using CodeAcademy_Console.Models;

namespace CodeAcademy_Console
{
    internal class GameEngine
    {

        internal void AdditionGame(string message)
        {
            var random = new Random();
            var score = 0;
            int firstNumber = random.Next(1, 9);
            int secondNumber = random.Next(1, 9);

            for (int i = 0; i < 5; i++)
            {
                Console.Clear();
                Console.WriteLine(message);
                firstNumber = random.Next(1, 9);
                secondNumber = random.Next(1, 9);
                Console.WriteLine($"{firstNumber} + {secondNumber}");
                var result = Console.ReadLine();

                result = Helpers.ValidateInput(result);

                if (int.Parse(result) == firstNumber + secondNumber)
                {
                    Console.WriteLine("Your answer is correct. Type any key for the next question.");
                    score++;
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Your answer is incorrect. Type any key for the next question.");
                    Console.ReadLine();
                }
                if (score == 4)
                {
                    Console.WriteLine($"Game over. Your final score is {score}. Press any key to go back to the main menu.");
                    Console.ReadLine();
                    break;
                }
            }
            Helpers.AddToHistory(score, GameType.Addition);

        }

        internal void SubtractionGame(string message)
        {

            var random = new Random();
            var score = 0;
            int firstNumber = random.Next(1, 9);
            int secondNumber = random.Next(1, 9);

            for (int i = 0; i < 5; i++)
            {
                Console.Clear();
                Console.WriteLine(message);
                firstNumber = random.Next(1, 9);
                secondNumber = random.Next(1, 9);
                Console.WriteLine($"{firstNumber} - {secondNumber}");
                var result = Console.ReadLine();

                result = Helpers.ValidateInput(result);

                if (int.Parse(result) == firstNumber - secondNumber)
                {
                    Console.WriteLine("Your answer is correct. Type any key for the next question.");
                    score++;
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Your answer is incorrect. Type any key for the next question.");
                    Console.ReadLine();
                }
                if (score == 4)
                {
                    Console.WriteLine($"Game over. Your final score is {score}. Press any key to go back to the main menu.");
                    Console.ReadLine();
                    break;
                }

            }
            Helpers.AddToHistory(score, GameType.Subtration);

        }

        internal void MultiplicationGame(string message)
        {

            var random = new Random();
            var score = 0;
            int firstNumber = random.Next(1, 9);
            int secondNumber = random.Next(1, 9);

            for (int i = 0; i < 5; i++)
            {
                Console.Clear();
                Console.WriteLine(message);
                firstNumber = random.Next(1, 9);
                secondNumber = random.Next(1, 9);
                Console.WriteLine($"{firstNumber} * {secondNumber}");
                var result = Console.ReadLine();

                result = Helpers.ValidateInput(result);

                if (int.Parse(result) == firstNumber * secondNumber)
                {
                    Console.WriteLine("Your answer is correct. Type any key for the next question.");
                    score++;
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Your answer is incorrect. Type any key for the next question.");
                    Console.ReadLine();
                }
                if (score == 4)
                {
                    Console.WriteLine($"Game over. Your final score is {score}. Press any key to go back to the main menu.");
                    Console.ReadLine();
                    break;
                }

            }
            Helpers.AddToHistory(score, GameType.Multiplication);
        }

        internal void DivisionGame(string message)
        {
            var score = 0;
            for (int i = 0; i < 5; i++)
            {
                Console.Clear();
                Console.WriteLine(message);
                var divisionNumber = Helpers.GetDivisionNubers();

                var firstNumber = divisionNumber[0];
                var secondNumber = divisionNumber[1];

                Console.WriteLine($"{firstNumber} / {secondNumber}");
                var result = Console.ReadLine();

                result = Helpers.ValidateInput(result);

                if (int.Parse(result) == firstNumber / secondNumber)
                {
                    Console.WriteLine("Your answer is correct. Type any key for the next question.");
                    score++;
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Your answer is incorrect. Type any key for the next question.");
                    Console.ReadLine();
                }
                if (score == 4)
                {
                    Console.WriteLine($"Game over. Your final score is {score}. Press any key to go back to the main menu.");
                    Console.ReadLine();
                    break;
                }
            }
            Helpers.AddToHistory(score, GameType.Division);

        }

    }
}