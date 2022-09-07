namespace Nibbles
{
    public record SnakePart
    {
        public GameObjectPosition Position { get; init; }

        public SnakePart(int x, int y)
        {
            Position = new GameObjectPosition(x, y);
        }
    }
}
