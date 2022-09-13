using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.UI
{
    public record TextSpritePart : Sprite
    {
        public TextSpritePart(Position position, ConsoleColor foregroundColor, ConsoleColor backgroundColor, char displayCharacter)
            : base(position, foregroundColor, backgroundColor, displayCharacter) { }
    }
}
