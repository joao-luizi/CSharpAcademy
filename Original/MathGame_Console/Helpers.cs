using System.Reflection.Emit;
using System.Text;

namespace Branch_Console;

internal class Helpers
{
    internal static DateTime GetDate()
    {
        return DateTime.UtcNow;
    }

    internal static int ConsoleWidth()
    {
        return Console.WindowWidth;
    }

    internal static string GetName()
    {
        var result = Console.ReadLine();
        while (string.IsNullOrEmpty(result))
        {
            Console.WriteLine("Name can't be empty");
            result = Console.ReadLine();
        }
        return result;
    }

    internal static List<double> GetOperands(Difficulty difficulty)
    {
        Random rnd = new();
        List<double> result = new();
        switch (difficulty)
        {
            case Difficulty.Easy:
                result.Add(rnd.Next(1, 9));
                result.Add(rnd.Next(1, 9));
                break;
            case Difficulty.Normal:
                result.Add(rnd.Next(-9, 9));
                result.Add(rnd.Next(-9, 9));
                result.Add(rnd.Next(-9, 9));
                break;
            case Difficulty.Hard:
                result.Add(Math.Round(rnd.Next(-19, 19) * rnd.NextDouble(), 1, MidpointRounding.AwayFromZero));
                result.Add(Math.Round(rnd.Next(-19, 19) * rnd.NextDouble(), 1, MidpointRounding.AwayFromZero));
                result.Add(Math.Round(rnd.Next(-19, 19) * rnd.NextDouble(), 1, MidpointRounding.AwayFromZero));
                result.Add(Math.Round(rnd.Next(-19, 19) * rnd.NextDouble(), 1, MidpointRounding.AwayFromZero));
                break;
            case Difficulty.Impossible:
                result.Add(Math.Round(rnd.Next(-29, 29) * rnd.NextDouble(), 1, MidpointRounding.AwayFromZero));
                result.Add(Math.Round(rnd.Next(-29, 29) * rnd.NextDouble(), 1, MidpointRounding.AwayFromZero));
                result.Add(Math.Round(rnd.Next(-29, 29) * rnd.NextDouble(), 1, MidpointRounding.AwayFromZero));
                result.Add(Math.Round(rnd.Next(-29, 29) * rnd.NextDouble(), 1, MidpointRounding.AwayFromZero));
                break;
        }
        return result;
    }

    internal static double? GetResult(MathOperation mathOp)
    {

        double? result = null;
        Parser parser = new Parser(mathOp.GetExpression());
        try
        {
            result = Math.Round(parser.ParseExpression().Eval(), 1, MidpointRounding.AwayFromZero);
        }
        catch (SyntaxException)
        {
            result = null;
        }
        return result;
    }

    internal static int[] GetOperationBounds(Difficulty difficulty)
    {
        var resultUpperBound = 0;
        var resultLowerBound = 0;
        switch (difficulty)
        {
            case Difficulty.Easy:
                resultUpperBound = 20;
                resultLowerBound = 1;
                break;
            case Difficulty.Normal:
                resultUpperBound = 40;
                resultLowerBound = -40;
                break;
            case Difficulty.Hard:
                resultUpperBound = 80;
                resultLowerBound = -80;
                break;
            case Difficulty.Impossible:
                resultUpperBound = 180;
                resultLowerBound = -180;
                break;
        }
        return new int[] { resultLowerBound, resultUpperBound };
    }

    internal static string GetOperationSymbol(OperationType operationType)
    {
        string operationSymbol = "";
        operationSymbol = operationType switch
        {
            OperationType.Addition => " + ",
            OperationType.Subtration => " - ",
            OperationType.Multiplication => " × ",
            OperationType.Division => " ÷ ",
            _ => "",
        };
        return operationSymbol;
    }

    internal static OperationType GetRandomGameType()
    {
        Random rnd = new Random();
        var toReturn = rnd.Next(1, 4) switch
        {
            1 => OperationType.Addition,
            2 => OperationType.Subtration,
            3 => OperationType.Multiplication,
            4 => OperationType.Division,
            _ => OperationType.Addition,
        };
        return toReturn;
    }

    internal static MathOperation GetOperation(OperationType? selectedGameType, Difficulty difficulty)
    {
        MathOperation newOperation = new MathOperation();
        int[] OperationResultBounds = GetOperationBounds(difficulty);
        double? operationResult = null;
        OperationType OpType;

        while (operationResult == null || operationResult < OperationResultBounds[0] || operationResult > OperationResultBounds[1])
        {
            newOperation = new MathOperation();
            newOperation.OperationDifficulty = difficulty;
            if (selectedGameType == null)
            {
                OpType = GetRandomGameType();
            }
            else
            {
                OpType = (OperationType)selectedGameType;
            }
            var operands = GetOperands(difficulty);
            BuildExpressions(OpType, operands, newOperation);

            operationResult = GetResult(newOperation);
        }
        newOperation.OperationResult = (double)operationResult;
        return newOperation;
    }
    internal static void BuildExpressions(OperationType OpType, List<double> operands, MathOperation mathOp)
    {

        List<string> tokens = new();
        List<string> userTokens = new();

        tokens.Add(operands[0].ToString());
        tokens.Add(GetOperationSymbol(OpType));
        tokens.Add(operands[1].ToString());
        userTokens.Add(operands[0].ToString());
        userTokens.Add(GetOperationSymbol(OpType));
        userTokens.Add(operands[1] < 0 ? $"({operands[1]})" : $"{operands[1]}");

        for (int i = 2; i < operands.Count; i++)
        {
            OperationType newOpType = GetRandomGameType();
            if (!OpType.HasFlag(newOpType))
            {
                OpType |= newOpType;
            }
            tokens.Add(GetOperationSymbol(newOpType));
            tokens.Add(operands[i].ToString());
            userTokens.Add(GetOperationSymbol(newOpType));
            userTokens.Add(operands[i] < 0 ? $"({operands[i]})" : $"{operands[i]}");
        }
        if (mathOp.OperationDifficulty == Difficulty.Impossible)
        {
            int index = tokens.FindLastIndex(x => x.Contains('+') || x.Contains('-'));
            if (index < 0)
            {
                index = tokens.FindLastIndex(x => x.Contains('×') || x.Contains('÷'));
            }
            var indexBefore = index - 2 < 0 ? 0 : index - 2;
            var indexAfter = index + 2 > tokens.Count ? tokens.Count : index + 2;

            tokens.Insert(indexBefore, "(");
            tokens.Insert(indexAfter, ")");
            userTokens.Insert(indexBefore, "(");
            userTokens.Insert(indexAfter, ")");

        }
        mathOp.Expressions = new List<string>() { string.Join(' ', tokens.ToArray()), string.Join(' ', userTokens.ToArray()) };
        mathOp.Operators = OpType;



    }

    internal static string OperationTitle(OperationType operationType)
    {
        string operationName = "";
        List<string> operators = new();
        if (operationType.HasFlag(OperationType.Addition))
        {
            operators.Add("Addition");
        }
        if (operationType.HasFlag(OperationType.Subtration))
        {
            operators.Add("Subtration");
        }
        if (operationType.HasFlag(OperationType.Multiplication))
        {
            operators.Add("Multiplication");
        }
        if (operationType.HasFlag(OperationType.Division))
        {
            operators.Add("Division");
        }
        operationName = $"{string.Join(',', operators.ToArray())} Game.";
        return operationName;
    }

    internal static int CalculateScore(Difficulty difficulty, int timerLeft)
    {
        int baseScore = 0;
        int timerBonus = 0;
        baseScore = difficulty switch
        {
            Difficulty.Easy => 1,
            Difficulty.Normal => 2,
            Difficulty.Hard => 3,
            Difficulty.Impossible => 4,
            _ => 0,
        };
        if (timerLeft >= 15)
        {
            timerBonus = 1;
        }
        return baseScore + timerBonus;
    }
    internal static double ValidateInput(Difficulty difficulty, string input)
    {
        string reply = "";
        double result;
        if (difficulty == Difficulty.Easy || difficulty == Difficulty.Normal)
        {
            reply = "Your answer need to be an Integer. Try again.";
        }
        else
        {
            reply = "Your answer need to be an Integer or a Double. Try again.";
        }
        while (!double.TryParse(input, out result))
        {
            Console.WriteLine($"{reply}");
            input = Console.ReadLine();
        }
        return result;
    }

    internal static string GetDifficultyDescription(int i)
    {
        string description = "";
        switch (i)
        {
            case 1:
                description = "Operands: Positive Integer from 1 to 9; Operations: one; Results: 1 to 20;";
                break;
            case 2:
                description = "Operands Type: Integer from -9 to 9; Operations: two; Results: -40 to 40;";
                break;
            case 3:
                description = "Operands Type: Double from -19 to 19; Operations: three; Results: -80 to 80;";
                break;
            case 4:
                description = "Operands Type: Double from -29 to 29; Operations: three and one Parenthesis; Results: -160 to 160;";
                break;
        }
        return description;
    }



}