namespace Nibbles.GameObject
{
    public class Food : ISprite
    {
        public Position Position { get; init; }

        private Random _random = new Random();

        public Food(Position position)
        {
            Position = position;
        }

        public Food(Position position, int number)
        {
            Position = position;
        }
    }
}
