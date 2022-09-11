namespace Nibbles.GameObject
{
    public record SnakePart : ISprite
    {
        private  Position _position;

        public SnakePart(int x, int y)
        {
            _position = new Position(x, y);
        }

        public Position GetPosition()
        {
            return _position;
        }
    }
}
