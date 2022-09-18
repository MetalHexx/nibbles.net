using Nibbles.GameObject.Configuration;

namespace Nibbles.Engine
{
    internal static class GameColorExtensions
    {
        public static ConsoleColor ToConsoleColor(this GameColor gameColor)
        {
            return gameColor switch
            {
                GameColor.Black => ConsoleColor.Black,
                GameColor.Cyan => ConsoleColor.Cyan,
                GameColor.Magenta => ConsoleColor.Magenta,
                GameColor.White => ConsoleColor.White,
                GameColor.Green => ConsoleColor.Green,
                GameColor.DarkBlue => ConsoleColor.DarkBlue,
                GameColor.Red => ConsoleColor.Red,
                _ => ConsoleColor.White
            };
        }
    }
}