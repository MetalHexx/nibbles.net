using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.Engine.Abstractions
{
    public abstract class GameManager : IGameManager
    {
        public Action? GameOver { get; set; }

        protected readonly ISpriteRenderer _renderer;

        public GameManager(ISpriteRenderer renderer)
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

        protected virtual void RegisterEvents(ISprite sprite)
        {
            sprite.SpriteCreated += OnSpriteCreated;
            sprite.SpriteDestroyed += OnSpriteDestroyed;
        }
    }
}
