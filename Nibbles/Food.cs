namespace Nibbles
{
    public record Food
    {
        public GameObjectPosition Position { get; init; }

        public Food(int x, int y)
        {
            Position = new GameObjectPosition(x, y);
        }

        public Food(GameObjectPosition position)
        {
            Position = position;
        }
    }
}
