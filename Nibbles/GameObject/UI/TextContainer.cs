using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public class GameText : SpriteContainer
    {
        public string Text { get; set; } = "";

        public GameText(Point position, int zIndex, string text,GameColor foregroundColor, GameColor backgroundColor)
            : base(position, zIndex, DirectionType.None, foregroundColor, backgroundColor, 0, 0) 
        {
            SetText(text);
        }
         public GameText(Point position, int zIndex, string text, DirectionType direction, GameColor foregroundColor, GameColor backgroundColor, double velocityX, double velocityY)
            : base(position, zIndex, direction, foregroundColor, backgroundColor, velocityX, velocityY)
        {
            SetText(text);
        }

        public virtual void SetText(string text)
        {
            _sprites.Clear();
            var lengthDelta = Text.Length - text.Length;
            Text = text;
            LeftJustify(lengthDelta);
            Build();
        }

        public void SetText(string text, GameColor foregroundColor, GameColor backgroundColor)
        {
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            SetText(text);
        }

        private void LeftJustify(int padAmount)
        {
            if (padAmount > 0)
            {
                Text = Text.PadRight(padAmount);
            }
        }

        protected override void Build()
        {
            var charList = Text.ToList();

            for (int i = 0; i < charList.Count; i++)
            {
                var position = Position with
                {
                    X = Position.X + i
                };
                Add(new TextSprite(position, ZIndex, ForegroundColor, BackgroundColor, charList[i]));
            }
        }
    }
}
