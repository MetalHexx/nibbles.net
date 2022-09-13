using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Abstractions
{
    public abstract record Sprite : ISprite
    {
        public Position _position { get; protected set; }
        public ConsoleColor ForegroundColor { get; protected set; }
        public ConsoleColor BackgroundColor { get; protected set; }
        public char DisplayCharacter { get; protected set; } = ' ';

        public Sprite(Position position, ConsoleColor foregroundColor, ConsoleColor backgroundColor, char displayCharacter)
        {
            _position = position;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            DisplayCharacter = displayCharacter;
        }

        public Position GetPosition()
        {
            return _position with { };
        }

        public virtual void Move(PositionTransform transform)
        {
            _position = _position with
            {
                XPosition = _position.XPosition + transform.XDelta,
                YPosition = _position.YPosition + transform.YDelta
            };
        }
    }
}
