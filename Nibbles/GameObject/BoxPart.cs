namespace Nibbles.GameObject
{
    public class BoxPart : ISprite
    {
        public char DisplayCharacter { get; set; } = ' ';

        public virtual ConsoleColor ForegroundColor { get; init; }

        public virtual ConsoleColor BackgroundColor { get; init; }

        private Position _position;

        public BoxPart(Position position, ConsoleColor foregroundColor, ConsoleColor backgrounColor)
        {
            _position = position;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgrounColor;
        }
        public Position GetPosition()
        {
            return _position;
        }
    }
}
