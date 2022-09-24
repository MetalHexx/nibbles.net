using Nibbles.Engine.Abstractions;

namespace Nibbles.Engine
{
    public class GameLoop
    {
        private readonly ISpriteRenderer _renderer;
        private readonly IGameManager _game;
        private bool _continueGame = true;
        private long _lastRenderTicks = DateTime.Now.Ticks; 

        public GameLoop(ISpriteRenderer renderer, IGameManager game)
        {
            _renderer = renderer;
            _game = game;
            _game.GameOver += () => _continueGame = false;
            _renderer.Render();            
        }
        public void Start()
        {
            do
            {   
                _game.GenerateFrame();
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
