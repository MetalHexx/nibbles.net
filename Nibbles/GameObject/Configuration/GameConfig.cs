namespace Nibbles.GameObject.Configuration
{
    public static class GameConfig
    {
        public const int MIN_FRAME_RENDER_SPEED_MS = 200;

        public const double SPRITE_DEFAULT_VELOCITY_X = 2;
        public const double SPRITE_DEFAULT_VELOCITY_Y = 1.8;
        public const int DEFAULT_SPRITE_HEALTH = 5;

        public const GameColor BOARD_BORDER_BACKGROUND_COLOR = GameColor.White;
        public const GameColor BOARD_BORDER_FOREGROUND_COLOR = GameColor.Black;
        public const GameColor BOARD_BACKGROUND_COLOR = GameColor.DarkBlue;
        public const GameColor BOARD_FOREGROUND_COLOR = GameColor.White;
        public const int BOARD_SIZE_X = 100;
        public const int BOARD_SIZE_Y = 20;
        public const int BOARD_ZINDEX = 0;
        public const int BOARD_TEXT_ZINDEX = 1;

        public const GameColor GAME_TEXTBOX_FOREGROUND_COLOR = GameColor.White;
        public const GameColor GAME_TEXTBOX_BACKGROUND_COLOR = GameColor.DarkMagenta;
        public const int GAME_TEXTBOX_ZINDEX = 2;
    }
}
