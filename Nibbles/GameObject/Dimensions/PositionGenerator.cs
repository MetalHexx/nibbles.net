using Nibbles.GameObject.Abstractions;
using System.Drawing;

namespace Nibbles.GameObject.Dimensions
{
    /// <summary>
    /// A helper class to assist with selecting positions to render game objects
    /// </summary>
    public static class PositionGenerator
    {
        private static Random _random = new Random();

        /// <summary>
        /// Gets a random available position given boundary and sprites to avoid rendering on top of
        /// </summary>
        /// <param name="boundaries">The bounding area in which the position select is constrained to</param>
        /// <param name="spritesToAvoid">A list of sprites whose position we want to avoid choosing</param>
        /// <returns>A random position given the contraints</returns>
        public static Point GetRandomPosition(AbsolutePosition boundaries, IEnumerable<ISprite> spritesToAvoid)
        {
            var positionsToAvoid = spritesToAvoid.Select(sprite => sprite.Position);
            return GetRandomPosition(boundaries, positionsToAvoid);
        }

        /// <summary>
        /// Gets a random available position given boundary and positions to avoid rendering on top of
        /// </summary>
        /// <param name="boundaries">The bounding area in which the position select is constrained to</param>
        /// <param name="spritesToAvoid">A list of positions we want to avoid choosing</param>
        /// <returns>A random position given the contraints</returns>
        public static Point GetRandomPosition(AbsolutePosition boundaries, IEnumerable<Point> positionsToAvoid)
        {
            var availablePositions = new List<Point>();

            for (int x = boundaries.MinX; x < boundaries.MaxX; x++)
            {
                for (int y = boundaries.MinY; y < boundaries.MaxY; y++)
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
