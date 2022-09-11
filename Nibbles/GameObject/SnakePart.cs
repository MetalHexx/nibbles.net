namespace Nibbles.GameObject
{
    public record SnakePart : ISprite
    {
        public Position Position { get; init; }

        public SnakePart(int x, int y)
        {
            Position = new Position(x, y);
        }
    }
}
