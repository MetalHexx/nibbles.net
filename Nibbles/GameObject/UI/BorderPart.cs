using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public record BorderPart : BoxPart
    {
        public BorderPart(Point position) :
            base(position, GameConfig.BOARD_BORDER_FOREGROUND_COLOR, GameConfig.BOARD_BORDER_BACKGROUND_COLOR)
        { }
    }
}
