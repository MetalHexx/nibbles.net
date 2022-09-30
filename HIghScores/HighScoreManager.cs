using Nibbles.Engine;
using System.Drawing;

namespace HighScores
{
    public class HighScoreManager
    {
        public void Start(Guid gameId)
        {
            CreateGame(gameId).Start();
        }

        private HighScoreLoop CreateGame(Guid gameId)
        {
            var inputReader = new KeyboardReader();
            var player = new Player(inputReader);
            var renderer = new SpriteRenderer();
            var scoreStore = new HighScoreStore();
            var highScoreReducer = new HighScoreReducer(renderer, player, new Size(Console.WindowWidth, Console.WindowHeight), scoreStore, gameId);
            return new HighScoreLoop(renderer, highScoreReducer);
        }
    }
}