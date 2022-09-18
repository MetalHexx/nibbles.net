using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.UI
{
    public record BoxPart : Sprite
    {
        public BoxPart(Position position, ConsoleColor foregroundColor, ConsoleColor backgrounColor)
            : base(position, DirectionType.None, foregroundColor, backgrounColor, ' ') { }
    }
}
