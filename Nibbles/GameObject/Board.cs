namespace Nibbles.GameObject
{
    public class Board : BorderBox 
    {
        public Board(): base(new BorderBoxDimensions(), SpriteConfig.BOARD_BORDER_FOREGROUND_COLOR, SpriteConfig.BOARD_BACKGROUND_COLOR)
        {

        }
    }    
}