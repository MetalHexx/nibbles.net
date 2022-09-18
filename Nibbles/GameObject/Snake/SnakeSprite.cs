using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Snake
{
    public record SnakeSprite : Sprite
    {
        public SnakeSprite(Position position, DirectionType direction)
            : base(position, direction, SpriteConfig.SNAKE_FOREGROUND_COLOR, SpriteConfig.SNAKE_BACKGROUND_COLOR, ' ', SpriteConfig.SNAKE_VELOCITY_X, SpriteConfig.SNAKE_VELOCITY_Y) { }

        /// <summary>
        /// Used for alternating color
        /// </summary>        
        public SnakeSprite(Position position, DirectionType direction, ConsoleColor backgroundColor)
            : base(position, direction, SpriteConfig.SNAKE_FOREGROUND_COLOR, backgroundColor, ' ', SpriteConfig.SNAKE_VELOCITY_X, SpriteConfig.SNAKE_VELOCITY_Y) { }
    }
}
