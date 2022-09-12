namespace Nibbles.GameObject
{
    public class BoardPart : ISprite
    {
        public char DisplayCharacter { get; set; } = ' ';

        public virtual ConsoleColor ForegroundColor => SpriteConfig.BOARD_BACKGROUND_COLOR;

        public virtual ConsoleColor BackgroundColor => SpriteConfig.BOARD_BACKGROUND_COLOR;

        private Position _position;

        public BoardPart(Position position)
        {
            _position = position;            
        }
        public Position GetPosition()
        {
            return _position;
        }
    }
}
