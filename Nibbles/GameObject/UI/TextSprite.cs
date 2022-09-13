using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.UI
{
    public class GameText : SpriteContainer
    {
        private string _text;

        public GameText(Position position, string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
            : base(position, foregroundColor, backgroundColor)
        {
            _text = text;
            Build();
        }

        public void SetText(string text)
        {
            var lengthDelta = _text.Length - text.Length;
            _text = text;
            PadWhiteSpace(lengthDelta);
            Build();
        }

        private void PadWhiteSpace(int padAmount)
        {
            if (padAmount > 0)
            {
                _text = _text.PadRight(padAmount);
            }
        }

        private void Build()
        {
            _sprites.Clear();
            var charList = _text.ToList();

            for (int i = 0; i < charList.Count; i++)
            {
                var position = Position with
                {
                    XPosition = Position.XPosition + i
                };
                _sprites.Add(new TextSpritePart(position, ForegroundColor, BackgroundColor, charList[i]));
            }
        }
    }
}
