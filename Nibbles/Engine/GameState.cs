using Nibbles.GameObject;

namespace Nibbles.Engine
{
    public class GameState
    {
        public Snake Snake { get; private set; }
        public Food? Food { get; private set; }
        public Board GameBoard { get; private set; } = new Board();
        private PositionGenerator _positionGenerator = new PositionGenerator();

        public Score Score { get; private set; } = new Score();

        public GameState()
        {
            Snake = new Snake();
            CreateFood();
        }

        public GameEvent DetermineGameEvents()
        {
            if (Snake.IsTouchingSelf || _gameBoardCollision)
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

        public List<ISprite?> GetGameObjects()
        {
            var gameObjects = Snake.GetParts()
                .Select(sp => sp as ISprite)
                .ToList();
            gameObjects.Add(Food);
            return gameObjects;
        }

        public void CreateFood()
        {
            var positionsToAvoidFoodPlacement = Snake
                        .GetParts()
                        .Select(sp => sp.Position)
                        .ToArray();

            var foodPosition = _positionGenerator.GetRandomPositionWithoutOverlap(GameBoard.MaxX - 1, GameBoard.MaxY - 1, positionsToAvoidFoodPlacement);

            Food = new Food(foodPosition);
        }

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
