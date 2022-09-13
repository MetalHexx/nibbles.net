using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.UI
{
    public class Board : BorderedBox
    {
        public Board(Position position, Size size) : base(position, size, SpriteConfig.BOARD_BORDER_FOREGROUND_COLOR, SpriteConfig.BOARD_BACKGROUND_COLOR) { }
    }
}