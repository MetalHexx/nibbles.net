namespace Nibbles
{
    public class Food: IGameObject
    {
        public GameObjectPosition Position { get; init; }

        private Random _random = new Random();

        public Food(GameObjectPosition position)
        {
            Position = position;
        }

        public Food(GameObjectPosition position, int number)
        {
            Position = position;
        }
    }
}
