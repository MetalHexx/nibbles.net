namespace Nibbles.GameObject
{
    public class BorderBoxDimensions
    {
        public int MinX { get; init; } = 0;
        public int MaxX { get; init; } = 100;
        public int MinY { get; init; } = 0;
        public int MaxY { get; init; } = 20;

        public BorderBoxDimensions() { }

        public BorderBoxDimensions(int minX, int maxX, int minY, int maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }
    }
}