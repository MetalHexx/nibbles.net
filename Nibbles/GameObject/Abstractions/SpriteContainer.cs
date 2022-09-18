using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.Abstractions
{
    public abstract class SpriteContainer : ISpriteContainer
    {        
        public Point Position { get; protected set; }
        protected DirectionType Direction { get; set; } = DirectionType.None;
        public ConsoleColor ForegroundColor { get; }
        public ConsoleColor BackgroundColor { get; }
        protected readonly List<ISprite> _sprites = new();

        public SpriteContainer(Point position, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Position = position;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public virtual void Move(PositionTransform transform, long timeDelta)
        {   
            foreach (var sprite in _sprites)
            {
                if (!sprite.ShouldMove(timeDelta)) return;
                sprite.Move(transform, timeDelta);
            }
            Position = _sprites.First().GetPosition();
        }

        public Point GetPosition()
        {
            return _sprites.First().GetPosition();
        }

        public IEnumerable<ISprite> GetSprites()
        {
            return _sprites;
        }
    }
}
