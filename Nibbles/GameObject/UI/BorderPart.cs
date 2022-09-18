using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public record BorderPart : BoxPart
    {
        public BorderPart(Point position) :
            base(position, SpriteConfig.BOARD_BORDER_FOREGROUND_COLOR, SpriteConfig.BOARD_BORDER_BACKGROUND_COLOR)
        { }
    }
}
