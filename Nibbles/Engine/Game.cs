using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.Engine
{
    public abstract class Game : IGame
    {   
        public Action? GameOver { get; set; }

        protected readonly ISpriteRenderer _renderer;

        public Game(ISpriteRenderer renderer)
        {
            _renderer = renderer;
        }
        protected abstract void InitializeSprites();
        public abstract void PlayerMove();
        public abstract void PlayerShoot();
        public abstract void UpdateState(PositionTransform transform, long renderDelta);
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

        protected virtual void RegisterSpriteEvents(ISprite sprite)
        {
            sprite.SpriteCreated += OnSpriteCreated;
            sprite.SpriteDestroyed += OnSpriteDestroyed;
        }
    }
}
