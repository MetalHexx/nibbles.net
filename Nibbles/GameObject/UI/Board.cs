using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public class Board : BorderedBox
    {
        public Board(Point position, Size size, int zIndex)
            : base(position, size, zIndex, DirectionType.None, GameConfig.BOARD_BORDER_FOREGROUND_COLOR, GameConfig.BOARD_BACKGROUND_COLOR, 0, 0) { }
    }
}