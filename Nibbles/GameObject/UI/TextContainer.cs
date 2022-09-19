using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public class GameText : SpriteContainer
    {
        private string _text;

        public GameText(Point position, string text, GameColor foregroundColor, GameColor backgroundColor)
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
                    X = Position.X + i
                };
                Add(new TextSprite(position, ForegroundColor, BackgroundColor, charList[i]));
            }
        }
    }
}
