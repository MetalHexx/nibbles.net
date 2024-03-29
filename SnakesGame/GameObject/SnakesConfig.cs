﻿using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;

namespace SnakesGame.GameObject
{
    public static class SnakesConfig
    {
        public static readonly Guid GAME_ID = Guid.Parse("BA19956D-6C56-4EEB-BF1C-8AA34D9C415F");
        public const int MIN_FRAME_RENDER_SPEED_MS = 200;

        public const double DEFAULT_SPRITE_VELOCITY_X = 2;
        public const double DEFAULT_SPRITE_VELOCITY_Y = 1.8;

        public const GameColor SNAKE_BACKGROUND_COLOR = GameColor.Cyan;
        public const GameColor SNAKE_FOREGROUND_COLOR = GameColor.Red;
        public const GameColor SNAKE_BACKGROUND_ALT_COLOR = GameColor.Green;
        public const int SNAKE_STARTING_POSITION_X = 5;
        public const int SNAKE_STARTING_POSITION_Y = 5;
        public const int SNAKE_ZINDEX = 1;
        public const DirectionType SNAKE_STARTING_DIRECTION = DirectionType.Right;
        public const double SNAKE_VELOCITY_X = 2;
        public const double SNAKE_VELOCITY_Y = 1.8;
        public static int SNAKE_GROWTH_PER_FEED = 5;

        public const GameColor VENOM_BACKGROUND_COLOR = GameColor.Green;
        public const GameColor VENOM_FOREGROUND_COLOR = GameColor.Green;
        public const int VENOM_DISTANCE_X = 20;
        public const int VENOM_DISTANCE_Y = 10;
        public const int VENOM_VELOCITY_X = 16;
        public const int VENOM_VELOCITY_Y = 8;
        public const int VENOM_ZINDEX = 1;


        public const GameColor FOOD_BACKGROUND_COLOR = GameColor.Red;
        public const GameColor FOOD_FOREGROUND_COLOR = GameColor.White;
        public const int FOOD_ZINDEX = 2;

        public const GameColor BOARD_BORDER_BACKGROUND_COLOR = GameColor.White;
        public const GameColor BOARD_BORDER_FOREGROUND_COLOR = GameColor.Black;
        public const GameColor BOARD_BACKGROUND_COLOR = GameColor.DarkBlue;
        public const GameColor BOARD_FOREGROUND_COLOR = GameColor.White;
        public const int BoardSizeX = 117;
        public const int BoardSizeY = 27;

        public const GameColor GAME_TEXTBOX_FOREGROUND_COLOR = GameColor.White;
        public const GameColor GAME_TEXTBOX_BACKGROUND_COLOR = GameColor.DarkMagenta;

        public const string GAME_TITLE = "Nibbles.net";
        public const string GAME_WIN = "Game Over! :)";
        public const string GAME_LOSE = "Game Over! :(";
    }
}
