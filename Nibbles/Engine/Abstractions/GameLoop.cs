using Nibbles.Engine.Abstractions;

namespace Nibbles.Engine
{
    public abstract class GameLoop : IGameLoop
    {
        public Action? GameOver { get; set; }
        private readonly ISpriteRenderer _renderer;
        private readonly IGameStateReducer _game;
        private bool _continueGame = true;
        private long _lastRenderTicks = DateTime.Now.Ticks;

        public GameLoop(ISpriteRenderer renderer, IGameStateReducer game)
        {
            _renderer = renderer;
            _game = game;
            _game.GameOver += () => _continueGame = false;
            _renderer.Render();
        }
        public virtual void Start()
        {
            do
            {
                _game.GenerateFrame();
                _renderer.Render();
            }
            while (_continueGame);

            GameOver?.Invoke();
        }

        protected virtual long GetTimeSinceRender()
        {
            var currentTick = DateTime.Now.Ticks;
            var delta = currentTick - _lastRenderTicks;
            _lastRenderTicks = currentTick;
            return delta;
        }
    }
}
