using Nibbles.GameObject.Configuration;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public class MenuItem : GameText
    {
        public string Name { get; private set; }
        private GameColor _default_Selected_Foreground_Color;
        private GameColor _default_Selected_Background_Color;
        private GameColor _default_Deselected_Foreground_Color;
        private GameColor _default_Deselected_Background_Color;
        public int Width { get; set; }
        public MenuItem(Point position, int zIndex, string text, int width, GameColor foregroundColor, GameColor backgroundColor, GameColor selectedForegroundColor, GameColor selectedBackgroundColor)
            : base(position, zIndex, text, foregroundColor, backgroundColor)
        {
            Name = text;
            _default_Deselected_Foreground_Color = foregroundColor;
            _default_Deselected_Background_Color = backgroundColor;
            _default_Selected_Foreground_Color = selectedForegroundColor;
            _default_Selected_Background_Color = selectedBackgroundColor;
            Width = width;
        }

        public void SelectItem()
        {
            ForegroundColor = _default_Selected_Foreground_Color;
            BackgroundColor = _default_Selected_Background_Color;
        }

        public void DeselectItem()
        {
            ForegroundColor = _default_Deselected_Foreground_Color;
            BackgroundColor = _default_Deselected_Background_Color;
        }

        /// <summary>
        /// Centers the menu item text.  If the width or text length are odd, the remaining space is added to the right.
        /// </summary>
        public override void SetText(string text)
        {
            if (Width == 0) return; //TODO: Fix this multiple calls to SetText problem.

            var paddingAmt = (Width - text.Length) / 2;
            var diff = paddingAmt % 2;
            diff = diff == 1 ? 0 : diff; //TODO: maybe improve this.  This fixes the issue with the longest menu item getting too much right padding.  We'll see if it holds.
            var centeredString = text.PadLeft(paddingAmt + text.Length, ' ');
            centeredString = centeredString.PadRight(paddingAmt + centeredString.Length + diff, ' ');
            _position.X = _position.X - (paddingAmt + diff);
            base.SetText(centeredString);
        }
    }
}
