using Nibbles.Engine.Abstractions;
using Nibbles.Player;

namespace Nibbles.Engine
{
    internal class GameEngine
    {
        private readonly ISpriteRenderer _renderer = new SpriteRenderer();
        private readonly GameState _gameState;
        private bool _continueGame = true;
        private readonly PlayerInput _player = new();

        public GameEngine()
        {
            _gameState = new GameState();
            _gameState.GameOver += () => _continueGame = false;
            _player.Moved += () => _gameState.IncrementMoveScore();
            _player.Shot += () => _gameState.SnakeShoot();
            Render();
        }
        public void Start()
        {
            do
            {
                Thread.Sleep(100);
                _player.UpdateState();               
                _gameState.DetectFoodCollision();
                _gameState.MoveSnake(_player.GetMove());
                _gameState.CheckGameBoardCollision();
                Render();
            }
            while (_continueGame);
        }

        private void Render()
        {
            var spritesToRender = _gameState.GetSpritesToRender();            
            _renderer.RenderSprites(spritesToRender);
        }
    }
}
