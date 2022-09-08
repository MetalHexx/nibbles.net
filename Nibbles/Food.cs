namespace Nibbles
{
    public record Food: IGameObject
    {
        public GameObjectPosition Position { get; init; }

        private static Random _random = new Random();

        public Food(int x, int y)
        {
            Position = new GameObjectPosition(x, y);
        }

        public Food(GameObjectPosition position)
        {
            Position = position;
        }

        public static Food? Create(params GameObjectPosition[] excludedPositions)
        {
            var newFoodPosition = GetRandomPosition();

            while (excludedPositions.Any(position => position == newFoodPosition))
            {
                newFoodPosition = GetRandomPosition();
            }
            return new Food(newFoodPosition);
        }

        private static GameObjectPosition GetRandomPosition() =>
            new GameObjectPosition(_random.Next(1, 10), _random.Next(1, 10));
    }
}
