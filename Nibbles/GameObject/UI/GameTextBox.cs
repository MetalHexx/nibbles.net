﻿using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.UI
{
    public class GameTextBox : BorderedBox
    {
        private GameText _text;
        public GameTextBox(string text, Point position, Size size)
            : base(position, size, SpriteConfig.GAME_TEXTBOX_FOREGROUND_COLOR, SpriteConfig.GAME_TEXTBOX_BACKGROUND_COLOR)
        {
            _text = new GameText(new Point(Dimensions.MinX + 2, Dimensions.MinY + 2), text,
                SpriteConfig.GAME_TEXTBOX_FOREGROUND_COLOR,
                SpriteConfig.GAME_TEXTBOX_BACKGROUND_COLOR);

            BuildTextBox();
        }

        public void BuildTextBox()
        {
            base.Build();
            _sprites.AddRange(_text.GetSprites());
        }

        public void SetText(string text)
        {
            _text.SetText(text);
            BuildTextBox();
        }
    }
}
