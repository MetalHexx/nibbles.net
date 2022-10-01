using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Tetris
{
    public record TetriminoPart : Sprite
    {
        public TetriminoPart(Point point, GameColor color)
            : base(point, 1, DirectionType.Down, color, color, ' ', 1, 1) { }
    }
}
