using Nibbles.GameObject;

namespace Nibbles.Engine
{
    internal class SpriteRenderer : ISpriteRenderer
    {  
        public SpriteRenderer()
        {
            Console.CursorVisible = false;
        }

        public void RenderSprites(SpriteRenderUpdate update)
        {
            Destroy(update.SpritesToRemove);
            Render(update.SpritesToAdd);
        }

        public void RenderBoard(Board board)
        {
            for (int x = board.MinX; x <= board.MaxX; x++)
            {
                for (int y = board.MinY; y <= board.MaxY; y++)
                {
                    var isBorder = x == board.MinX
                        || y == board.MinY
                        || x == board.MaxX
                        || y == board.MaxY;

                    WriteText(" ", 
                        SpriteConfig.BOARD_BACKGROUND_COLOR, 
                        SpriteConfig.BOARD_BACKGROUND_COLOR, 
                        x, y);

                    if (isBorder) WriteText(" ", 
                        SpriteConfig.BOARD_BORDER_BACKGROUND_COLOR, 
                        SpriteConfig.BOARD_BORDER_BACKGROUND_COLOR, 
                        x, y);
                }
            }
            WriteText(SpriteConfig.GAME_TITLE, SpriteConfig.BOARD_BORDER_FOREGROUND_COLOR, SpriteConfig.BOARD_BORDER_BACKGROUND_COLOR, 1, 0);
        }

        public void RenderScore(GameState state)
        {
            var scorePrefix = "";

            WriteText(SpriteConfig.CLEAR_SCORE_TEXT,
                SpriteConfig.BOARD_BORDER_FOREGROUND_COLOR,
                SpriteConfig.BOARD_BORDER_BACKGROUND_COLOR,
                SpriteConfig.GAME_TITLE.Length + 1, 0);

            WriteText($" | Amount Eaten: {state.Score.AmountEaten} | Moves: {state.Score.Moves} | Score: {state.Score.Total}",
                SpriteConfig.BOARD_BORDER_FOREGROUND_COLOR,
                SpriteConfig.BOARD_BORDER_BACKGROUND_COLOR,
                SpriteConfig.GAME_TITLE.Length + 1, 0);
        }

        private void Render(IEnumerable<ISprite?> gameObjects)
        {
            foreach (var obj in gameObjects)
            {
                if (obj is null) continue;

                var objMetaData = SpriteConfig.GetSpriteMetadata(obj);

                if (objMetaData is null) throw new Exception("A game object was found to be null");

                WriteText(objMetaData.Text,
                    objMetaData.ForegroundColor,
                    objMetaData.BackgroundColor,
                    obj.GetPosition().XPosition,
                    obj.GetPosition().YPosition);
            }
        }

        private void Destroy(IEnumerable<ISprite?> gameObjects)
        {
            foreach (var obj in gameObjects)
            {
                if (obj is null) continue;

                var objMetaData = SpriteConfig.GetSpriteMetadata(obj);

                if (objMetaData is null) throw new Exception("A game object was found to be null");

                WriteText(ReplaceTextWithEmptyString(objMetaData.Text),
                    SpriteConfig.BOARD_BACKGROUND_COLOR,
                    SpriteConfig.BOARD_BACKGROUND_COLOR,
                    obj.GetPosition().XPosition,
                    obj.GetPosition().YPosition);
            }
        }

        private string ReplaceTextWithEmptyString(string text)
        {
            var charArray = text.ToCharArray();
            foreach (var character in charArray)
            {
                text.Replace(character, ' ');
            }
            return text;
        }

        private void WriteText(string text, 
            ConsoleColor foregroundColor, 
            ConsoleColor backgroundColor, 
            int xPosition, 
            int yPosition)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write(text);
            Console.ResetColor();
        }
    }
}