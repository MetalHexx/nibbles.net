namespace Nibbles.GameObject
{
    public class TextSpritePart: ISprite
    {
        public char DisplayCharacter { get; set; }
        public ConsoleColor ForegroundColor { get; private set; }
        public ConsoleColor BackgroundColor { get; private set; }

        private Position _position;

        public TextSpritePart(Position position, char displayCharacter, ConsoleColor foregroundColor, ConsoleColor backgrounColor)
        {
            _position = position;
            DisplayCharacter = displayCharacter;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgrounColor;
        }

        public Position GetPosition()
        {
            return _position;
        }
    }
}
