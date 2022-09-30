using Nibbles.GameObject.Abstractions;

namespace Nibbles.Engine.Abstractions
{
    public abstract class GameStateReducer : IGameStateReducer
    {
        public Action? GameOver { get; set; }

        protected readonly ISpriteRenderer _renderer;
        protected long _lastRenderTicks = DateTime.Now.Ticks;

        public GameStateReducer(ISpriteRenderer renderer)
        {
            _renderer = renderer;
        }

        public abstract void GenerateFrame();
        protected abstract void HandleGameWin(string text);
        protected abstract void HandleGameOver(string text);

        protected virtual void OnSpriteCreated(ISprite sprite)
        {
            _renderer.Add(sprite);
        }

        protected virtual void OnSpriteDestroyed(ISprite sprite)
        {
            _renderer.Remove(sprite);
        }

        protected virtual void RegisterEvents(ISprite sprite)
        {
            sprite.SpriteCreated += OnSpriteCreated;
            sprite.SpriteDestroyed += OnSpriteDestroyed;
        }

        public long GetTimeSinceLastFrame()
        {
            var currentTick = DateTime.Now.Ticks;
            var delta = currentTick - _lastRenderTicks;

            if (delta == 0) return _lastRenderTicks;

            _lastRenderTicks = currentTick;
            return delta;
        }
    }
}
