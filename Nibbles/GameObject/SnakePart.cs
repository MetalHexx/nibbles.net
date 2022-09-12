namespace Nibbles.GameObject
{
    public record SnakePart : ISprite
    {
        public virtual char DisplayCharacter { get; } = ' ';
        public ConsoleColor ForegroundColor => SpriteConfig.SNAKE_FOREGROUND_COLOR;
        public ConsoleColor BackgroundColor => SpriteConfig.SNAKE_BACKGROUND_COLOR;

        private  Position _position;

        public SnakePart(int x, int y)
        {
            _position = new Position(x, y);
        }

        public Position GetPosition()
        {
            return _position;
        }
    }
}
