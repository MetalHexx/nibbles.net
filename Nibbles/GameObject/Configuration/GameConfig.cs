﻿namespace Nibbles.GameObject.Configuration
{
    public static class GameConfig
    {
        public const int MIN_FRAME_RENDER_SPEED_MS = 200;

        public const double DEFAULT_SPRITE_VELOCITY_X = 2;
        public const double DEFAULT_SPRITE_VELOCITY_Y = 1.8;

        public const GameColor BOARD_BORDER_BACKGROUND_COLOR = GameColor.White;
        public const GameColor BOARD_BORDER_FOREGROUND_COLOR = GameColor.Black;
        public const GameColor BOARD_BACKGROUND_COLOR = GameColor.DarkBlue;
        public const GameColor BOARD_FOREGROUND_COLOR = GameColor.White;
        public const int BoardSizeX = 100;
        public const int BoardSizeY = 20;

        public const GameColor GAME_TEXTBOX_FOREGROUND_COLOR = GameColor.White;
        public const GameColor GAME_TEXTBOX_BACKGROUND_COLOR = GameColor.Magenta;
    }
}
