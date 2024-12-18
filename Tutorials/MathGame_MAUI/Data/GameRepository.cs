using SQLite;
using MathGame.CSharAcademy.Models;

namespace MathGame.CSharAcademy.Data
{
    public class GameRepository
    {
        string _dbPath;
        private SQLiteConnection conn;

        public GameRepository(string dbPath)
        {
            _dbPath = dbPath;
            Init();
        }

        public void Init()
        {
            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Game>();
        }

        public List<Game> GetAllGames()
        {
            return conn.Table<Game>().ToList();
        }
        public void AddGame(Game game)
        {
            conn = new SQLiteConnection(_dbPath);
            conn.Insert(game);
        }

        public void DeleteGame(int id) {
            conn = new SQLiteConnection(_dbPath);
            conn.Delete(new Game { Id = id});
        }
    }
}
