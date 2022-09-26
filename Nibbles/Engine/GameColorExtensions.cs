using Nibbles.GameObject.Configuration;

namespace Nibbles.Engine
{
    internal static class GameColorExtensions
    {
        public static ConsoleColor ToConsoleColor(this GameColor gameColor)
        {
            return gameColor switch
            {
                GameColor.Red => ConsoleColor.Red,                
                GameColor.Yellow => ConsoleColor.Yellow,
                GameColor.Blue => ConsoleColor.Blue,
                GameColor.Green => ConsoleColor.Green,
                GameColor.Cyan => ConsoleColor.Cyan,
                GameColor.Magenta => ConsoleColor.Magenta,

                GameColor.DarkRed => ConsoleColor.DarkRed,
                GameColor.DarkYellow => ConsoleColor.DarkYellow,
                GameColor.DarkBlue => ConsoleColor.DarkBlue,
                GameColor.DarkGreen => ConsoleColor.DarkGreen,
                GameColor.DarkCyan => ConsoleColor.DarkCyan,
                GameColor.DarkMagenta => ConsoleColor.DarkMagenta,

                GameColor.White => ConsoleColor.White,
                GameColor.Black => ConsoleColor.Black,
                GameColor.Gray => ConsoleColor.Gray,
                GameColor.DarkGray => ConsoleColor.DarkGray,
                _ => ConsoleColor.White
            };
        }
    }
}