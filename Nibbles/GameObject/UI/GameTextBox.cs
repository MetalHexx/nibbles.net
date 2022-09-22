using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public class GameTextBox : BorderedBox
    {
        private GameText _text;
        public GameTextBox(string text, Point position, Size size)
            : base(position, GameConfig.GAME_TEXTBOX_ZINDEX, size, DirectionType.None, GameConfig.GAME_TEXTBOX_FOREGROUND_COLOR, GameConfig.GAME_TEXTBOX_BACKGROUND_COLOR, 0, 0)
        {
            _text = new GameText(
                new Point(Dimensions.MinX + 2, Dimensions.MinY + 2), GameConfig.GAME_TEXTBOX_ZINDEX, text, DirectionType.None,
                GameConfig.GAME_TEXTBOX_FOREGROUND_COLOR,
                GameConfig.GAME_TEXTBOX_BACKGROUND_COLOR, 0, 0);

            _text.SpriteCreated += Add;
        }

        public void SetText(string text)
        {
            Build();
            _text.SetText(text);            
        }
    }
}
