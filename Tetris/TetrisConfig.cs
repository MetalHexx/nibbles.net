using Nibbles.GameObject.Configuration;

internal class TetrisConfig
{
    public static readonly Guid GAME_ID = Guid.Parse("0AC2342F-F9D9-4C7A-98F0-07992F6B52CF");
    public const int MIN_FRAME_RENDER_SPEED_MS = 200;

    public const double DEFAULT_SPRITE_VELOCITY_X = 2;
    public const double DEFAULT_SPRITE_VELOCITY_Y = 1.8;

    public const string GAME_TITLE = "Tetris";
    public const string GAME_WIN = "Game Over! :)";
    public const string GAME_LOSE = "Game Over! :(";

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
}