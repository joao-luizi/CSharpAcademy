namespace Branch_Console;

// Exception for syntax errors
internal class SyntaxException : Exception
{
    public SyntaxException(string message)
        : base(message)
    {
    }
}