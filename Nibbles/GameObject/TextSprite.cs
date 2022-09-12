namespace Nibbles.GameObject
{
    public class GameText: ISpriteContainer
    {
        public virtual ConsoleColor ForegroundColor { get; protected set; }
        public virtual ConsoleColor BackgroundColor { get; protected set; }

        private string _text;
        private List<TextSpritePart> _parts = new List<TextSpritePart>();
        private Position _position;


        public GameText(Position position, string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            _position = position;
            _text = text;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            BuildSprite();
        }

        public void SetText(string text)
        {
            var lengthDelta = _text.Length - text.Length;
            _text = text;
            PadWhiteSpace(lengthDelta);
            BuildSprite();
        }

        private void PadWhiteSpace(int padAmount)
        {
            if(padAmount > 0)
            {
                _text.PadRight(padAmount);
            }
        }

        public Position GetPosition()
        {
            return _position;
        }

        public IEnumerable<ISprite> GetSprites()
        {
            return _parts;
        }

        private void BuildSprite()
        {
            _parts.Clear();
            var charList = _text.ToList();

            for (int i = 0; i < charList.Count(); i++)
            {
                var position = _position with
                {
                    XPosition = _position.XPosition + i
                };
                _parts.Add(new TextSpritePart(position, charList[i], ForegroundColor, BackgroundColor));
            }
        }
    }
}
