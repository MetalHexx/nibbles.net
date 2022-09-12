namespace Nibbles.GameObject
{
    public class Food : ISprite
    {
        public ConsoleColor ForegroundColor => SpriteConfig.FOOD_FOREGROUND_COLOR;
        public ConsoleColor BackgroundColor => SpriteConfig.FOOD_BACKGROUND_COLOR;
        public char DisplayCharacter { get; } = ' ';

        private Position _position;
        private Random _random = new Random();

        public Food(Position position)
        {
            _position = position;
        }

        public Food(Position position, int number)
        {
            _position = position;
        }

        public Position GetPosition()
        {
            return _position;
        }
    }
}
