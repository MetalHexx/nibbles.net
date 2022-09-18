using System.Drawing;

namespace Nibbles.GameObject.Dimensions
{
    public class PositionGenerator
    {
        private Random _random = new Random();

        public Point GetUniqueRandomPosition(int maxX, int maxY, params Point[] excludedPositions)
        {
            var newFoodPosition = GetRandomPosition(maxX, maxY);

            while (excludedPositions.Any(position => position == newFoodPosition))
            {
                newFoodPosition = GetRandomPosition(maxX, maxY);
            }
            return newFoodPosition;
        }

        private Point GetRandomPosition(int maxX, int maxY) =>
            new Point(_random.Next(1, maxX), _random.Next(1, maxY));
    }
}
