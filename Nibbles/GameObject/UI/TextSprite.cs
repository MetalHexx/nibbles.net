using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public record TextSprite : Sprite
    {
        public TextSprite(Point position, ConsoleColor foregroundColor, ConsoleColor backgroundColor, char displayCharacter)
            : base(position, DirectionType.None, foregroundColor, backgroundColor, displayCharacter) { }
    }
}
