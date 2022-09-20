using Nibbles.GameObject.Abstractions;
using System.Drawing;

namespace Nibbles.GameObject.Dimensions
{
    public static class PositionGenerator
    {
        private static Random _random = new Random();

        public static Point GetRandomPosition(int maxX, int maxY, IEnumerable<ISprite> spritesToAvoid)
        {
            var positionsToAvoid = spritesToAvoid.Select(sprite => sprite.Position);
            return GetRandomPosition(maxX, maxY, positionsToAvoid);
        }

        public static Point GetRandomPosition(int maxX, int maxY, IEnumerable<Point> positionsToAvoid)
        {
            var availablePositions = new List<Point>();

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    var point = new Point(x, y);

                    if(!positionsToAvoid.Any(pos => pos == point))
                    {
                        availablePositions.Add(point);
                    }
                }
            }
            return availablePositions[_random.Next(0, availablePositions.Count)];
        }
    }
}
