namespace Nibbles.GameObject.Dimensions
{
    public record AbsolutePosition
    {
        public int MinX { get; }
        public int MaxX { get; }
        public int MinY { get; }
        public int MaxY { get; }

        public AbsolutePosition(Position position, Size size)
        {
            MinX = position.XPosition;
            MaxX = position.XPosition + size.Width;
            MinY = position.YPosition;
            MaxY = position.YPosition + size.Height;
        }
    }
}