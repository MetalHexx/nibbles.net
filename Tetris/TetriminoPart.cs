using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Tetris
{
    public record TetriminoPart : Sprite
    {
        public TetriminoPart(Point point, GameColor color, double velocityY, TimeSpan timeSinceMove)
            : base(point, 1, DirectionType.Down, color, color, ' ', 0, velocityY, timeSinceMove) { }
    }
}
