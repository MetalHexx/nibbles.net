namespace Nibbles
{
    internal class GameObjectRenderer : IGameObjectRenderer
    {
        private const ConsoleColor SNAKE_COLOR = ConsoleColor.Cyan;
        private const ConsoleColor FOOD_BACKGROUND_COLOR = ConsoleColor.Red;
        private const ConsoleColor FOOD_FOREGROUND_COLOR = ConsoleColor.White;
        private const ConsoleColor BOARD_BORDER_BACKGROUND_COLOR = ConsoleColor.White;
        private const ConsoleColor BOARD_BORDER_FOREGROUND_COLOR = ConsoleColor.Black;
        private const ConsoleColor BOARD_BACKGROUND_COLOR = ConsoleColor.DarkBlue;
        private const string GAME_TITLE = "Nibbles.net";
        public GameObjectRenderer()
        {
            Console.CursorVisible = false;            
        }
        public void Render(IEnumerable<IGameObject?> gameObjects)
        {
            foreach (var obj in gameObjects)
            {
                if (obj is null) continue;

                var objMetaData = GetGameObjectMetadata(obj);
                
                if (objMetaData is null) throw new Exception("A game object was found to be null");

                WriteText(objMetaData.Text, objMetaData.ForegroundColor, objMetaData.BackgroundColor, obj.Position.XPosition, obj.Position.YPosition);
            }
        }

        public void Clear(IEnumerable<IGameObject?> gameObjects)
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

        public void RenderBoard(GameBoard board)
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
            var scorePrefix = " | Amount Eaten: ";
            var previousScore = $"{scorePrefix}{state.PreviousAmountEaten}           ";

            WriteText(ReplaceTextWithEmptyString(previousScore),
                BOARD_BORDER_FOREGROUND_COLOR,
                BOARD_BORDER_BACKGROUND_COLOR,
                GAME_TITLE.Length + 1, 0);

            WriteText($"{scorePrefix}{state.AmountEaten}           ", 
                BOARD_BORDER_FOREGROUND_COLOR, 
                BOARD_BORDER_BACKGROUND_COLOR, 
                GAME_TITLE.Length + 1, 0);
        }

        private GameObjectMetadata? GetGameObjectMetadata(IGameObject gameObject)
        {
            switch (gameObject)
            {
                case SnakePart snake:
                    return new GameObjectMetadata
                    {
                        BackgroundColor = SNAKE_COLOR,
                        ForegroundColor = SNAKE_COLOR,
                        Text = " "
                    };

                case Food food:
                    return new GameObjectMetadata
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