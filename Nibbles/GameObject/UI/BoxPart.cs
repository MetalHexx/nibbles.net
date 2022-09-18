using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public record BoxPart : Sprite
    {
        public BoxPart(Point position, GameColor foregroundColor, GameColor backgrounColor)
            : base(position, DirectionType.None, foregroundColor, backgrounColor, ' ') { }
    }
}
