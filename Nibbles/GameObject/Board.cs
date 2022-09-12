namespace Nibbles.GameObject
{
    public class Board: ISpriteContainer
    {
        public int MinX { get; private set; } = 0;
        public int MaxX { get; private set; } = 100;
        public int MinY { get; private set; } = 0;
        public int MaxY { get; private set; } = 20;

        private readonly List<BoardPart> _parts = new List<BoardPart>();

        public Board()
        {
            GenerateBoard();
        }

        public Position GetPosition()
        {
            return _parts.First().GetPosition();
        }

        public IEnumerable<ISprite> GetParts()
        {
            return _parts;
        }

        private void GenerateBoard()
        {
            for (int x = MinX; x <= MaxX; x++)
            {
                for (int y = MinY; y <= MaxY; y++)
                {
                    var isBorder = x == MinX
                        || y == MinY
                        || x == MaxX
                        || y == MaxY;

                    if (isBorder)
                    {
                        _parts.Add(new BoardBorderPart(new Position(x, y)));
                    }
                    else
                    {
                        _parts.Add(new BoardPart(new Position(x, y)));
                    }
                }
            }
        }
    }
}