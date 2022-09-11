namespace Nibbles.GameObject
{
    public class Food : ISprite
    {
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
