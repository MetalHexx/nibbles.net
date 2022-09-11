using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibbles
{
    public class PositionGenerator
    {
        private Random _random = new Random();

        public GameObjectPosition GetRandomPositionWithoutOverlap(int maxXPosition, int maxYPosition, params GameObjectPosition[] excludedPositions)
        {
            var newFoodPosition = GetRandomPosition(maxXPosition, maxYPosition);

            while (excludedPositions.Any(position => position == newFoodPosition))
            {
                newFoodPosition = GetRandomPosition(maxXPosition, maxYPosition);
            }
            return newFoodPosition;
        }

        private GameObjectPosition GetRandomPosition(int maxXPosition, int maxYPosition) =>
            new GameObjectPosition(_random.Next(1, maxXPosition), _random.Next(1, maxYPosition));
    }
}
