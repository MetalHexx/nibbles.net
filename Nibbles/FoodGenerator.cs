using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibbles
{
    public class FoodGenerator : ScreenWriter
    {
        private Random _random = new Random();
        public Food? Next(IEnumerable<GameObjectPosition> excludedPositions)
        {
            var newFoodPosition = GetRandomPosition();

            while (excludedPositions.Any(position => position == newFoodPosition))
            {
                newFoodPosition = GetRandomPosition();
            }
            WriteCharacter('F', ConsoleColor.Red, newFoodPosition.XPosition, newFoodPosition.YPosition);
            return new Food(newFoodPosition);
        }

        public GameObjectPosition GetRandomPosition() => 
            new GameObjectPosition(_random.Next(1, 10), _random.Next(1, 10));
    }
}
