using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public record BoxPart : Sprite
    {
        public BoxPart(Point position, ConsoleColor foregroundColor, ConsoleColor backgrounColor)
            : base(position, DirectionType.None, foregroundColor, backgrounColor, ' ') { }
    }
}
