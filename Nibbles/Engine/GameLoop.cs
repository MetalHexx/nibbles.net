using Nibbles.Engine.Abstractions;
using Nibbles.Player;

namespace Nibbles.Engine
{
    public class GameLoop
    {
        private readonly ISpriteRenderer _renderer;
        private readonly IGame _game;
        private readonly IPlayerInput _player;
        private bool _continueGame = true;
        private long _lastRenderTicks = DateTime.Now.Ticks; 

        public GameLoop(IPlayerInput player, ISpriteRenderer renderer, IGame game)
        {
            _player = player;
            _renderer = renderer;
            _game = game;
            _game.GameOver += () => _continueGame = false;
            _player.Moved += _game.PlayerMove;
            _player.Shot += _game.PlayerShoot;
            _renderer.Render();            
        }
        public void Start()
        {
            do
            {   
                _player.UpdateState();
                _game.UpdateState(_player.GetMove(), GetTimeSinceRender());
                _renderer.Render();
            }
            while (_continueGame);
        }

        public long GetTimeSinceRender()
        {
            var currentTick = DateTime.Now.Ticks;
            var delta = currentTick - _lastRenderTicks;
            _lastRenderTicks = currentTick;
            return delta;
        }
    }
}
