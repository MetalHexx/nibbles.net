using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Snake
{
    public record SnakePart : Sprite
    {
        public SnakePart(Position position)
            : base(position, SpriteConfig.SNAKE_FOREGROUND_COLOR, SpriteConfig.SNAKE_BACKGROUND_COLOR, ' ') { }
    }
}
