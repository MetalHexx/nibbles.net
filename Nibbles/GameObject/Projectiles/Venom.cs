using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Projectiles
{
    public record Venom: Sprite
    {
        public Venom(Position position)
            : base(position, SpriteConfig.VENOM_FOREGROUND_COLOR, SpriteConfig.VENOM_BACKGROUND_COLOR, ' ') { }
    }
}
