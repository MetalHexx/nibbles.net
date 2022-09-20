using Nibbles.Engine.Abstractions;
using Nibbles.Player;
using System.Drawing;

namespace Nibbles.Engine
{
    internal class Engine
    {
        private readonly ISpriteRenderer _renderer;
        private readonly IGameStateHandler _actions;
        private readonly ICollisionDetector _collisionDetector;
        private readonly IPlayerInput _player;
        private bool _continueGame = true;
        private long _lastRenderTicks = DateTime.Now.Ticks; 

        public Engine(IPlayerInput player, ISpriteRenderer renderer, IGameStateHandler actions, ICollisionDetector collisionDetector)
        {
            _player = player;
            _renderer = renderer;
            _actions = actions;
            _collisionDetector = collisionDetector;
            _actions.GameOver += () => _continueGame = false;
            _player.Moved += _actions.IncrementMoveScore;
            _player.Shot += _actions.SnakeShoot;
            _renderer.Render();            
        }
        public void Start()
        {
            do
            {
                var timeDelta = GetTimeSinceRender();
                _player.UpdateState();
                _collisionDetector.Detect();
                _actions.UpdateSprites(_player.GetMove(), timeDelta);
                _renderer.Render();
            }
            while (_continueGame);
        }

        public long GetTimeSinceRender()
        {
            var currentTick = DateTime.Now.Ticks;
            var delta = currentTick - _lastRenderTicks;
            var timeSpan = new TimeSpan(delta);
            _lastRenderTicks = currentTick;
            return delta;
        }
    }
}
