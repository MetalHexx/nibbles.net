namespace Nibbles.GameObject
{
    public class GameTextBox : BorderBox
    {
        private GameText _text;
        public GameTextBox(string text, BorderBoxDimensions dimensions) 
            : base(dimensions, SpriteConfig.GAME_TEXTBOX_FOREGROUND_COLOR, SpriteConfig.GAME_TEXTBOX_BACKGROUND_COLOR) 
        {
            _text = new GameText(new Position(dimensions.MinX + 1, dimensions.MinY + 1), text,
                SpriteConfig.GAME_TEXTBOX_FOREGROUND_COLOR,
                SpriteConfig.GAME_TEXTBOX_BACKGROUND_COLOR);

            BuildTextBox();
        }

        public void BuildTextBox()
        {
            base.Build();            
            _parts.AddRange(_text.GetSprites());
        }

        public void SetText(string text)
        {
            _text.SetText(text);
            BuildTextBox();
        }
    }
}
