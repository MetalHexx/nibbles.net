using System.Drawing;

namespace Nibbles.GameObject.Dimensions
{
    /// <summary>
    /// Used to define the position of a sprite with absolute coordinates
    /// </summary>
    public record AbsolutePosition
    {
        public int MinX { get; }
        public int MaxX { get; }
        public int MinY { get; }
        public int MaxY { get; }
        public Point Position { get; }
        public Size Size { get; }

        public AbsolutePosition(Point position, Size size)
        {
            MinX = position.X;
            MaxX = position.X + size.Width - 1;
            MinY = position.Y;
            MaxY = position.Y + size.Height - 1;
            Position = position;
            Size = size;
        }
    }
}