using HighScores;
using Nibbles.Engine;
using SnakesGame.GameObject;
using System.Drawing;

namespace SnakesGame.Engine
{
    public class SnakesManager
    {
        private readonly HighScoreManager _highScores = new HighScoreManager();
        public void Start()
        {
            CreateGame().Start();
            Console.ReadLine();
            _highScores.Start(SnakesConfig.GAME_ID);            
        }

        private SnakesGameLoop CreateGame()
        {
            var inputReader = new KeyboardReader();
            var player = new Player(inputReader);
            var renderer = new SpriteRenderer();
            var gameState = new GameState(player, new Size(Console.WindowWidth, Console.WindowHeight));
            var collisionDetector = new CollisionDetector(gameState);
            var scoreStore = new HighScoreStore();
            var soundGenerator = new SoundGenerator();
            var snakesReducer = new SnakesStateReducer(gameState, renderer, collisionDetector, scoreStore, soundGenerator);
            return new SnakesGameLoop(renderer, snakesReducer);
        }
    }
}
