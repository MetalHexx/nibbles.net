using Nibbles.GameObject;

namespace Nibbles.Engine
{
    internal class SpriteRenderer : ISpriteRenderer
    {
        private const ConsoleColor SNAKE_COLOR = ConsoleColor.Cyan;
        private const ConsoleColor FOOD_BACKGROUND_COLOR = ConsoleColor.Red;
        private const ConsoleColor FOOD_FOREGROUND_COLOR = ConsoleColor.White;
        private const ConsoleColor BOARD_BORDER_BACKGROUND_COLOR = ConsoleColor.White;
        private const ConsoleColor BOARD_BORDER_FOREGROUND_COLOR = ConsoleColor.Black;
        private const ConsoleColor BOARD_BACKGROUND_COLOR = ConsoleColor.DarkBlue;
        private const string GAME_TITLE = "Nibbles.net";
        private const string CLEAR_SCORE_TEXT = "                                                               ";
        public SpriteRenderer()
        {
            Console.CursorVisible = false;
        }
        public void Render(IEnumerable<ISprite?> gameObjects)
        {
            foreach (var obj in gameObjects)
            {
                if (obj is null) continue;

                var objMetaData = GetGameObjectMetadata(obj);

                if (objMetaData is null) throw new Exception("A game object was found to be null");

                WriteText(objMetaData.Text, objMetaData.ForegroundColor, objMetaData.BackgroundColor, obj.Position.XPosition, obj.Position.YPosition);
            }
        }

        public void Clear(IEnumerable<ISprite?> gameObjects)
        {
            foreach (var obj in gameObjects)
            {
                if (obj is null) continue;

                var objMetaData = GetGameObjectMetadata(obj);

                if (objMetaData is null) throw new Exception("A game object was found to be null");

                WriteText(ReplaceTextWithEmptyString(objMetaData.Text),
                    BOARD_BACKGROUND_COLOR,
                    BOARD_BACKGROUND_COLOR,
                    obj.Position.XPosition,
                    obj.Position.YPosition);
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

                    WriteText(" ", BOARD_BACKGROUND_COLOR, BOARD_BACKGROUND_COLOR, x, y);
                    if (isBorder) WriteText(" ", BOARD_BORDER_BACKGROUND_COLOR, BOARD_BORDER_BACKGROUND_COLOR, x, y);
                }
            }
            WriteText(GAME_TITLE, BOARD_BORDER_FOREGROUND_COLOR, BOARD_BORDER_BACKGROUND_COLOR, 1, 0);
        }

        public void RenderScore(GameState state)
        {
            var scorePrefix = "";

            WriteText(CLEAR_SCORE_TEXT,
                BOARD_BORDER_FOREGROUND_COLOR,
                BOARD_BORDER_BACKGROUND_COLOR,
                GAME_TITLE.Length + 1, 0);

            WriteText($" | Amount Eaten: {state.Score.AmountEaten} | Moves: {state.Score.Moves} | Score: {state.Score.Total}",
                BOARD_BORDER_FOREGROUND_COLOR,
                BOARD_BORDER_BACKGROUND_COLOR,
                GAME_TITLE.Length + 1, 0);
        }

        private SpriteMetadata? GetGameObjectMetadata(ISprite gameObject)
        {
            switch (gameObject)
            {
                case SnakePart snake:
                    return new SpriteMetadata
                    {
                        BackgroundColor = SNAKE_COLOR,
                        ForegroundColor = SNAKE_COLOR,
                        Text = " "
                    };

                case Food food:
                    return new SpriteMetadata
                    {
                        BackgroundColor = FOOD_BACKGROUND_COLOR,
                        ForegroundColor = FOOD_FOREGROUND_COLOR,
                        Text = $" "
                    };

                default:
                    return null;
            }
        }

        private void WriteText(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor, int xPosition, int yPosition)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write(text);
            Console.ResetColor();
        }
    }
}