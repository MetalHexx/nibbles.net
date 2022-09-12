namespace Nibbles.GameObject
{
    public class BorderPart : BoxPart 
    {
        public BorderPart(Position position) : 
            base(position, SpriteConfig.BOARD_BORDER_FOREGROUND_COLOR, SpriteConfig.BOARD_BORDER_BACKGROUND_COLOR) { }
    }
}
