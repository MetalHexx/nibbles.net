namespace Nibbles
{
    public class SnakeInputHandler
    {
        public SnakeDirection GetDirection()
        {
            if (!Console.KeyAvailable) return SnakeDirection.NoChange;

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    return SnakeDirection.Up;

                case ConsoleKey.UpArrow:
                    return SnakeDirection.Up;

                case ConsoleKey.S:
                    return SnakeDirection.Down;

                case ConsoleKey.DownArrow:
                    return SnakeDirection.Down;

                case ConsoleKey.A:
                    return SnakeDirection.Left;

                case ConsoleKey.LeftArrow:
                    return SnakeDirection.Left;

                case ConsoleKey.D:
                    return SnakeDirection.Right;

                case ConsoleKey.RightArrow:
                    return SnakeDirection.Right;

                default:
                    return SnakeDirection.NoChange;
            }
        }
    }
}
