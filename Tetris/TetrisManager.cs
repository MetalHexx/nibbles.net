using HighScores;
using Nibbles.Engine;
using System.Drawing;

namespace Tetris
{
    public class TetrisManager
    {
        private readonly HighScoreManager _highScores = new HighScoreManager();
        public void Start()
        {
            CreateGame().Start();
            //_highScores.Start(TetrisConfig.GAME_ID);
        }

        private TetrisLoop CreateGame()
        {
            var inputReader = new KeyboardReader();
            var player = new Player(inputReader);
            var renderer = new SpriteRenderer();
            var gameState = new TetrisState(player, new Size(Console.WindowWidth, Console.WindowHeight));
            var collisionDetector = new CollisionDetector(gameState);
            var scoreStore = new HighScoreStore();
            var soundGenerator = new SoundGenerator();
            var snakesReducer = new TetrisReducer(gameState, renderer, collisionDetector, scoreStore, soundGenerator);
            return new TetrisLoop(renderer, snakesReducer);
        }
    }
}
