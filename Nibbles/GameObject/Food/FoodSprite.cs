using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Food
{
    public record FoodSprite : Sprite
    {
        public FoodSprite(Position position)
            : base(position, SpriteConfig.FOOD_FOREGROUND_COLOR, SpriteConfig.FOOD_BACKGROUND_COLOR, ' ') { }
    }
}
