using Nibbles.Engine.Abstractions;
using Nibbles.Player;
using System.Drawing;

namespace Nibbles.Engine
{
    internal class Engine
    {
        private readonly ISpriteRenderer _renderer;
        private readonly IGameStateHandler _actions;
        private readonly IPlayerInput _player;
        private bool _continueGame = true;

        public Engine(IPlayerInput player, ISpriteRenderer renderer, IGameStateHandler actions)
        {
            _player = player;
            _renderer = renderer;
            _actions = actions;
            _actions.GameOver += () => _continueGame = false;
            _player.Moved += () => _actions.IncrementMoveScore();
            _player.Shot += () => _actions.SnakeShoot();
            Render();            
        }
        public void Start()
        {
            do
            {
                Thread.Sleep(100);
                _player.UpdateState();               
                _actions.DetectFoodCollision();
                _actions.MoveSnake(_player.GetMove());
                _actions.CheckGameBoardCollision();
                Render();
            }
            while (_continueGame);
        }

        private void Render()
        {
            var spritesToRender = _actions.GetSpritesToRender();
            _renderer.RenderSprites(spritesToRender);
        }
    }
}
