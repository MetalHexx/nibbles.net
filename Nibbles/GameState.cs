namespace Nibbles
{
    public class GameState
    {
        public Snake Snake { get; set; } = new Snake();
        public Food? Food { get; set; }
        public GameBoard GameBoard { get; set; } = new GameBoard();

        public GameState(Snake initialSnake, Food? initialFood)
        {
            Snake = initialSnake;
            Food = initialFood;
        }

        public GameEvent DetermineGameEvents()
        {
            if (_touchedSelf || _gameBoardCollision)
            {
                Console.WriteLine("You lose! :(");
                return GameEvent.Lose;
            }
            if (Food is null)
            {
                Console.WriteLine("You win! :)");
                return GameEvent.Win;
            }
            return GameEvent.Continue;
        }

        public List<IGameObject?> GetGameObjects()
        {
            var gameObjects = Snake.GetParts()
                .Select(sp => sp as IGameObject)
                .ToList();
            gameObjects.Add(Food);
            return gameObjects;
        }

        private bool _touchedSelf => Snake.GetParts()
            .Skip(1)
            .Any(snakePart => Snake.Position == snakePart.Position);

        private bool _gameBoardCollision =>
            Snake.Position.XPosition == GameBoard.MinX
            ||
            Snake.Position.XPosition == GameBoard.MaxX
            ||
            Snake.Position.YPosition == GameBoard.MinY
            ||
            Snake.Position.YPosition == GameBoard.MaxY;
    }
}
