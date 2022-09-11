namespace Nibbles.GameObject
{
    public class PositionGenerator
    {
        private Random _random = new Random();

        public Position GetRandomPositionWithoutOverlap(int maxXPosition, int maxYPosition, params Position[] excludedPositions)
        {
            var newFoodPosition = GetRandomPosition(maxXPosition, maxYPosition);

            while (excludedPositions.Any(position => position == newFoodPosition))
            {
                newFoodPosition = GetRandomPosition(maxXPosition, maxYPosition);
            }
            return newFoodPosition;
        }

        private Position GetRandomPosition(int maxXPosition, int maxYPosition) =>
            new Position(_random.Next(1, maxXPosition), _random.Next(1, maxYPosition));
    }
}
