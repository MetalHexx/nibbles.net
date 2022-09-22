using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace SnakesGame.GameObject
{
    public record SnakePart : Sprite
    {
        public SnakePart(Point position, DirectionType direction)
            : base(position, direction, SnakesConfig.SNAKE_FOREGROUND_COLOR, SnakesConfig.SNAKE_BACKGROUND_COLOR, ' ', SnakesConfig.SNAKE_VELOCITY_X, SnakesConfig.SNAKE_VELOCITY_Y) { }

        /// <summary>
        /// Used for alternating color
        /// </summary>        
        public SnakePart(Point position, DirectionType direction, GameColor backgroundColor)
            : base(position, direction, SnakesConfig.SNAKE_FOREGROUND_COLOR, backgroundColor, ' ', SnakesConfig.SNAKE_VELOCITY_X, SnakesConfig.SNAKE_VELOCITY_Y) { }
    }
}
