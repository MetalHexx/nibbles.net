using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.Food
{
    public record FoodSprite : Sprite
    {
        public FoodSprite(Point position)
            : base(position, DirectionType.None, SpriteConfig.FOOD_FOREGROUND_COLOR, SpriteConfig.FOOD_BACKGROUND_COLOR, ' ') { }
    }
}
