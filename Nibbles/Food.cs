namespace Nibbles
{
    public class Food: IGameObject
    {
        public GameObjectPosition Position { get; init; }

        private static Random _random = new Random();

        public Food(int x, int y, GameObjectPosition[] excludedPositions)
        {
            Position = GetRandomPosition(x, y, excludedPositions);
        }

        public Food(GameObjectPosition position, int number)
        {
            Position = position;
        }

        private GameObjectPosition GetRandomPosition(int maxXPosition, int maxYPosition, params GameObjectPosition[] excludedPositions)
        {
            var newFoodPosition = GetRandomPosition(maxXPosition, maxYPosition);

            while (excludedPositions.Any(position => position == newFoodPosition))
            {
                newFoodPosition = GetRandomPosition(maxXPosition, maxYPosition);
            }
            return newFoodPosition;
        }

        private static GameObjectPosition GetRandomPosition(int maxXPosition, int maxYPosition) =>
            new GameObjectPosition(_random.Next(1, maxXPosition), _random.Next(1, maxYPosition));
    }
}
