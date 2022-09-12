namespace Nibbles.GameObject
{
    public class BoardBorderPart : BoardPart 
    {
        public override ConsoleColor ForegroundColor => SpriteConfig.BOARD_BORDER_FOREGROUND_COLOR;

        public override ConsoleColor BackgroundColor => SpriteConfig.BOARD_BORDER_BACKGROUND_COLOR;
        public BoardBorderPart(Position position) : base(position) { }
    }
}
