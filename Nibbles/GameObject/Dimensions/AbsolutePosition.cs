using System.Drawing;

namespace Nibbles.GameObject.Dimensions
{
    public record AbsolutePosition
    {
        public int MinX { get; }
        public int MaxX { get; }
        public int MinY { get; }
        public int MaxY { get; }

        public AbsolutePosition(Point position, Size size)
        {
            MinX = position.X;
            MaxX = position.X + size.Width;
            MinY = position.Y;
            MaxY = position.Y + size.Height;
        }
    }
}