using Nibbles.Engine.Abstractions;
using Nibbles.Player;

namespace Nibbles.Engine
{
    public class KeyboardReader : IInputReader
    {
        public InputType Get()
        {
            return !Console.KeyAvailable
                ? InputType.NoInput
                : Console.ReadKey(true).Key switch
                {
                    ConsoleKey.W => InputType.Up,
                    ConsoleKey.UpArrow => InputType.Up,
                    ConsoleKey.S => InputType.Down,
                    ConsoleKey.DownArrow => InputType.Down,
                    ConsoleKey.A => InputType.Left,
                    ConsoleKey.LeftArrow => InputType.Left,
                    ConsoleKey.D => InputType.Right,
                    ConsoleKey.RightArrow => InputType.Right,
                    ConsoleKey.Spacebar => InputType.Spacebar,
                    _ => InputType.NoInput
                };
        }
    }
}
