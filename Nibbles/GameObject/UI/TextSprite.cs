using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public record TextSprite : Sprite
    {
        public TextSprite(Point position, int zIndex, GameColor foregroundColor, GameColor backgroundColor, char displayCharacter)
            : base(position, zIndex, DirectionType.None, foregroundColor, backgroundColor, displayCharacter) { }
    }
}
