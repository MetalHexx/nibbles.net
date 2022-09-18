using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public class Board : BorderedBox
    {
        public Board(Point position, Size size) : base(position, size, SpriteConfig.BOARD_BORDER_FOREGROUND_COLOR, SpriteConfig.BOARD_BACKGROUND_COLOR) { }
    }
}