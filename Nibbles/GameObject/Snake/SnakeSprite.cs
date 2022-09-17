using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Snake
{
    public record SnakePart : Sprite
    {
        private const int VELOCITY = 2;
        public SnakePart(Position position)
            : base(position, SpriteConfig.SNAKE_FOREGROUND_COLOR, SpriteConfig.SNAKE_BACKGROUND_COLOR, ' ', VELOCITY) { }

        public SnakePart(Position position, ConsoleColor backgroundColor)
            : base(position, SpriteConfig.SNAKE_FOREGROUND_COLOR, backgroundColor, ' ', VELOCITY) { }

        public SnakePart(Position position, ConsoleColor backgroundColor, char character)
            : base(position, SpriteConfig.SNAKE_FOREGROUND_COLOR, backgroundColor, character, VELOCITY) { }
    }
}
