namespace Nibbles
{
    internal class GameObjectRenderer : IGameObjectRenderer
    {
        private const ConsoleColor SNAKE_COLOR = ConsoleColor.Green;
        private const ConsoleColor FOOD_COLOR = ConsoleColor.Red;
        private const ConsoleColor CLEAR_COLOR = ConsoleColor.Black;
        private const char SPACE_CHAR = ' ';
        public GameObjectRenderer()
        {
            Console.CursorVisible = false;            
        }
        public void Render(IEnumerable<IGameObject?> gameObjects)
        {
            foreach (var obj in gameObjects)
            {
                if (obj is null) continue;
                WriteCharacter(SPACE_CHAR, GetGameObjectColor(obj), obj.Position.XPosition, obj.Position.YPosition);
            }
        }

        public void Clear(IEnumerable<IGameObject?> gameObjects)
        {
            foreach (var obj in gameObjects)
            {
                if (obj is null) continue;
                WriteCharacter(SPACE_CHAR, CLEAR_COLOR, obj.Position.XPosition, obj.Position.YPosition);
            }
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

                    if(isBorder) WriteCharacter(' ', ConsoleColor.Cyan, x, y);
                }
            }
        }

        private ConsoleColor GetGameObjectColor(IGameObject gameObject)
        {
            switch (gameObject)
            {
                case SnakePart snakePartType:
                    return SNAKE_COLOR;

                case Food foodType:
                    return FOOD_COLOR;

                default:
                    return CLEAR_COLOR;
            }
        }

        private void WriteCharacter(char character, ConsoleColor color, int xPosition, int yPosition)
        {
            Console.ForegroundColor = color;
            Console.BackgroundColor = color;
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write(character);
            Console.ResetColor();
        }
    }
}
