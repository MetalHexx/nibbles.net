namespace Nibbles
{
    public record SnakePart: IGameObject
    {
        public GameObjectPosition Position { get; init; }

        public SnakePart(int x, int y)
        {
            Position = new GameObjectPosition(x, y);
        }
    }
}
