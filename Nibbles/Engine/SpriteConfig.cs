using Nibbles.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibbles.Engine
{
    public static class SpriteConfig
    {
        public const ConsoleColor SNAKE_COLOR = ConsoleColor.Cyan;
        public const ConsoleColor FOOD_BACKGROUND_COLOR = ConsoleColor.Red;
        public const ConsoleColor FOOD_FOREGROUND_COLOR = ConsoleColor.White;
        public const ConsoleColor BOARD_BORDER_BACKGROUND_COLOR = ConsoleColor.White;
        public const ConsoleColor BOARD_BORDER_FOREGROUND_COLOR = ConsoleColor.Black;
        public const ConsoleColor BOARD_BACKGROUND_COLOR = ConsoleColor.DarkBlue;
        public const string GAME_TITLE = "Nibbles.net";
        public const string CLEAR_SCORE_TEXT = "";

        public static SpriteMetadata? GetSpriteMetadata(ISprite gameObject)
        {
            switch (gameObject)
            {
                case SnakePart snake:
                    return new SpriteMetadata
                    {
                        BackgroundColor = SpriteConfig.SNAKE_COLOR,
                        ForegroundColor = SpriteConfig.SNAKE_COLOR,
                        Text = " "
                    };

                case Food food:
                    return new SpriteMetadata
                    {
                        BackgroundColor = SpriteConfig.FOOD_BACKGROUND_COLOR,
                        ForegroundColor = SpriteConfig.FOOD_FOREGROUND_COLOR,
                        Text = $" "
                    };

                default:
                    return null;
            }
        }
    }
}
