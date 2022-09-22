using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace SnakesGame.GameObject
{
    public record Food : Sprite
    {
        public Food(Point position)
           : base(position, DirectionType.None, SnakesConfig.FOOD_FOREGROUND_COLOR, SnakesConfig.FOOD_BACKGROUND_COLOR, ' ') { }
    }
}
