namespace Nibbles.GameObject
{

    public class BorderBox : ISpriteContainer
    {
        public BorderBoxDimensions Dimensions { get; private set; } = new BorderBoxDimensions();

        protected readonly List<ISprite> _parts = new List<ISprite>();
        protected ConsoleColor _foregroundColor;
        protected ConsoleColor _backgroundColor;

        public BorderBox(BorderBoxDimensions dimensions, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Dimensions = dimensions;
            _foregroundColor = foregroundColor;
            _backgroundColor = backgroundColor;
            Build();
        }

        public Position GetPosition()
        {
            return _parts.First().GetPosition();
        }

        public IEnumerable<ISprite> GetSprites()
        {
            return _parts;
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
                        _parts.Add(new BorderPart(new Position(x, y)));
                    }
                    else
                    {
                        _parts.Add(new BoxPart(new Position(x, y), _foregroundColor, _backgroundColor));
                    }
                }
            }
        }
    }
}