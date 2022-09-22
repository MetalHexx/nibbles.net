using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public class GameText : SpriteContainer
    {
        private string _text = "";

         public GameText(Point position, string text, DirectionType direction, GameColor foregroundColor, GameColor backgroundColor, double velocityX, double velocityY)
            : base(position, direction, foregroundColor, backgroundColor, velocityX, velocityY)
        {
            SetText(text);
        }

        public void SetText(string text)
        {
            _sprites.Clear();
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

        protected override void Build()
        {
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
