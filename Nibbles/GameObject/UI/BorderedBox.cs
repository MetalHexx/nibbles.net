using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public class BorderedBox : SpriteContainer
    {
        public AbsolutePosition Dimensions { get; private set; }
        public Size Size { get; set; }

        public BorderedBox(Point position, int zIndex, Size size, DirectionType direction, GameColor foregroundColor, GameColor backgroundColor, double velocityX, double velocityY)
            : base(position, zIndex, direction, foregroundColor, backgroundColor, velocityX, velocityY)
        {
            Size = size;
            Dimensions = new AbsolutePosition(position, size);
            Build();
        }

        protected override void Build()
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
                        Add(new BorderPart(new Point(x, y), ZIndex));
                    }
                    else
                    {
                        Add(new BoxPart(new Point(x, y), ZIndex, ForegroundColor, BackgroundColor));
                    }
                }
            }
        }
    }
}