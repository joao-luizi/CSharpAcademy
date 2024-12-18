namespace CodeAcademy_Console.Models
{
    internal class Game
    {
        /* private int _score;
        public int Score
        {
            get { return _score}
            set { _score = value; }
        } */
        internal int Score { get; set; }
        internal DateTime Date { get; set; }

        internal GameType Type { get; set; }
    }

}

internal enum GameType
{
    Addition, Subtration, Multiplication, Division
}