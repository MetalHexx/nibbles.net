using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Configuration
{
    public static class SpriteConfig
    {
        public const int MIN_FRAME_RENDER_SPEED_MS = 200; 

        public const double DEFAULT_SPRITE_VELOCITY_X = 2;
        public const double DEFAULT_SPRITE_VELOCITY_Y = 1.8;

        public const ConsoleColor SNAKE_BACKGROUND_COLOR = ConsoleColor.Cyan;
        public const ConsoleColor SNAKE_FOREGROUND_COLOR = ConsoleColor.Red;
        public const ConsoleColor SNAKE_BACKGROUND_ALT_COLOR = ConsoleColor.Green;
        public const int SNAKE_STARTING_POSITION_X = 5;
        public const int SNAKE_STARTING_POSITION_Y = 5;
        public const DirectionType SNAKE_STARTING_DIRECTION = DirectionType.Right;
        public const double SNAKE_VELOCITY_X = 2;
        public const double SNAKE_VELOCITY_Y = 1.8;

        public const ConsoleColor VENOM_BACKGROUND_COLOR = ConsoleColor.Green;
        public const ConsoleColor VENOM_FOREGROUND_COLOR = ConsoleColor.Green;
        public const int VENOM_DISTANCE_X = 20;        
        public const int VENOM_DISTANCE_Y = 10;
        public const int VENOM_VELOCITY_X = 16;
        public const int VENOM_VELOCITY_Y = 8;


        public const ConsoleColor FOOD_BACKGROUND_COLOR = ConsoleColor.Red;
        public const ConsoleColor FOOD_FOREGROUND_COLOR = ConsoleColor.White;

        public const ConsoleColor BOARD_BORDER_BACKGROUND_COLOR = ConsoleColor.White;
        public const ConsoleColor BOARD_BORDER_FOREGROUND_COLOR = ConsoleColor.Black;
        public const ConsoleColor BOARD_BACKGROUND_COLOR = ConsoleColor.DarkBlue;
        public const ConsoleColor BOARD_FOREGROUND_COLOR = ConsoleColor.White;

        public const ConsoleColor GAME_TEXTBOX_FOREGROUND_COLOR = ConsoleColor.White;
        public const ConsoleColor GAME_TEXTBOX_BACKGROUND_COLOR = ConsoleColor.Magenta;

        public const string GAME_TITLE = "Nibbles.net";
        public const string GAME_WIN = "Game Over! :)";
        public const string GAME_LOSE = "Game Over! :(";
    }
}
