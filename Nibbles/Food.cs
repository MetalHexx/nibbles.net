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

        public static Food? Create(int maxXPosition, int maxYPosition, params GameObjectPosition[] excludedPositions)
        {
            var newFoodPosition = GetRandomPosition(maxXPosition, maxYPosition);

            while (excludedPositions.Any(position => position == newFoodPosition))
            {
                newFoodPosition = GetRandomPosition(maxXPosition, maxYPosition);
            }
            return new Food(newFoodPosition);
        }

        private static GameObjectPosition GetRandomPosition(int maxXPosition, int maxYPosition) =>
            new GameObjectPosition(_random.Next(1, maxXPosition), _random.Next(1, maxYPosition));
    }
}
