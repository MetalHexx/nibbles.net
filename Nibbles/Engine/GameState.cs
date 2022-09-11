using Nibbles.GameObject;

namespace Nibbles.Engine
{
    public class GameState
    {
        public event Action? GameLost, GameWon;
        public Snake Snake { get; private set; }
        public Food? Food { get; private set; }
        public Board GameBoard { get; private set; } = new Board();
        private PositionGenerator _positionGenerator = new PositionGenerator();

        public Score Score { get; private set; } = new Score();

        public GameState()
        {
            Snake = new Snake();
            Snake.TouchedSelf += OnTouchedSelf;
            CreateFood();
        }

        public void OnTouchedSelf()
        {
            GameLost?.Invoke();
        }

        public List<ISprite?> GetGameObjects()
        {
            var gameObjects = Snake.GetParts().ToList();
            gameObjects.Add(Food);
            return gameObjects;
        }

        public void CreateFood()
        {
            var positionsToAvoidFoodPlacement = Snake
                        .GetParts()
                        .Select(sp => sp.Position)
                        .ToArray();

            var totalPossible = (GameBoard.MaxX - 1) * (GameBoard.MaxY - 1);

            if (totalPossible == positionsToAvoidFoodPlacement.Length)
            {
                GameWon?.Invoke();
            }

            var foodPosition = _positionGenerator.GetUniqueRandomPosition(GameBoard.MaxX - 1, GameBoard.MaxY - 1, positionsToAvoidFoodPlacement);

            Food = new Food(foodPosition);
        }

        public void CheckGameBoardCollision()
        {
            var collisionCondition = 
                Snake.Position.XPosition == GameBoard.MinX
                ||
                Snake.Position.XPosition == GameBoard.MaxX
                ||
                Snake.Position.YPosition == GameBoard.MinY
                ||
                Snake.Position.YPosition == GameBoard.MaxY;

            if (collisionCondition) GameLost?.Invoke();
        }
    }
            
}
