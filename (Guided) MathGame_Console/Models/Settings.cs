namespace Branch_Console;

internal class Settings
{
    internal string PlayerName { get; set; }
    internal Difficulty Difficulty { get; set; }
    internal int QuestionsCount { get; set; }
}

internal enum Difficulty : int
{
    Easy = 1,
    Normal = 2,
    Hard = 3,
    Impossible = 4

}