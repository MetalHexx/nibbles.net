namespace Nibbles
{
    public class SnakeInputHandler
    {
        public GameObjectDirection GetDirection()
        {
            if (!Console.KeyAvailable) return GameObjectDirection.NoChange;

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    return GameObjectDirection.Up;

                case ConsoleKey.UpArrow:
                    return GameObjectDirection.Up;

                case ConsoleKey.S:
                    return GameObjectDirection.Down;

                case ConsoleKey.DownArrow:
                    return GameObjectDirection.Down;

                case ConsoleKey.A:
                    return GameObjectDirection.Left;

                case ConsoleKey.LeftArrow:
                    return GameObjectDirection.Left;

                case ConsoleKey.D:
                    return GameObjectDirection.Right;

                case ConsoleKey.RightArrow:
                    return GameObjectDirection.Right;

                default:
                    return GameObjectDirection.NoChange;
            }
        }
    }
}
