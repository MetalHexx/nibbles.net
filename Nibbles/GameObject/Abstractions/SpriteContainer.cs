using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Abstractions
{
    public abstract class SpriteContainer : ISpriteContainer
    {
        public Position Position { get; }
        public ConsoleColor ForegroundColor { get; }
        public ConsoleColor BackgroundColor { get; }
        protected readonly List<ISprite> _sprites = new();
        public SpriteContainer(Position position, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Position = position;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public virtual void Move(PositionTransform transform)
        {
            foreach (var sprite in _sprites)
            {
                sprite.Move(transform);
            }
        }

        public Position GetPosition()
        {
            return _sprites.First().GetPosition();
        }

        public IEnumerable<ISprite> GetSprites()
        {
            return _sprites;
        }
    }
}
