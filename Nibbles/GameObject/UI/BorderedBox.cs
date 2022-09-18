using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public class BorderedBox : SpriteContainer
    {
        public AbsolutePosition Dimensions { get; private set; }
        public Size Size { get; set; }

        public BorderedBox(Point position, Size size, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
            : base(position, foregroundColor, backgroundColor)
        {
            Size = size;
            Dimensions = new AbsolutePosition(position, size);
            Build();
        }

        protected virtual void Build()
        {
            for (int x = Dimensions.MinX; x <= Dimensions.MaxX; x++)
            {
                for (int y = Dimensions.MinY; y <= Dimensions.MaxY; y++)
                {
                    var isBorder = x == Dimensions.MinX
                        || y == Dimensions.MinY
                        || x == Dimensions.MaxX
                        || y == Dimensions.MaxY;

                    if (isBorder)
                    {
                        _sprites.Add(new BorderPart(new Point(x, y)));
                    }
                    else
                    {
                        _sprites.Add(new BoxPart(new Point(x, y), ForegroundColor, BackgroundColor));
                    }
                }
            }
        }
    }
}