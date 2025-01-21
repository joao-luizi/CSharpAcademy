using System.Linq.Expressions;

namespace Branch_Console;

internal class MathOperation
{
    //the main operation type will allways exist in the expression and will be the first operator
    internal OperationType Operators { get; set; }
    internal Difficulty OperationDifficulty { get; set; }
    internal List<string> Expressions { get; set; }
    internal double OperationResult { get; set; }

    public override string ToString()
    {
        return $"{GetExpression()} = {OperationResult}";
    }
    public string GetExpression()
    {
        if (Expressions.Count >= 1)
        {
            return $"{Expressions[0]}";
        }
        else
        {
            return "";
        }
    }

    public string GetUserExpression()
    {
        if (Expressions.Count >= 2)
        {
            return $"{Expressions[1]}";
        }
        else
        {
            return "";
        }
    }


}

[Flags]
internal enum OperationType : int
{
    None = 0,
    Addition = 1,
    Subtration = 2,
    Multiplication = 4,
    Division = 8
}