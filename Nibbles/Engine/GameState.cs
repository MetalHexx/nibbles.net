using Nibbles.GameObject;

namespace Nibbles.Engine
{
    public class GameState
    {
        public event Action? GameLost, GameWon, FoodEaten;
        private Snake _snake;
        private Food? _food;
        public Board GameBoard { get; private set; } = new Board();
        private PositionGenerator _positionGenerator = new PositionGenerator();

        public Score Score { get; private set; } = new Score();

        public GameState()
        {
            _snake = new Snake();
            _snake.TouchedSelf += OnTouchedSelf;
            CreateFood();
        }

        public void OnTouchedSelf()
        {
            GameLost?.Invoke();
        }

        public List<ISprite?> GetGameObjects()
        {
            var gameObjects = _snake.GetParts().ToList();
            gameObjects.Add(_food);
            return gameObjects;
        }
        public void FeedSnake()
        {
            _snake.Feed();
            Score.IncrementAmountEaten();
        }

        public void CreateFood()
        {
            var positionsToAvoidFoodPlacement = _snake
                        .GetParts()
                        .Select(sp => sp.Position)
                        .ToArray();

            var totalPossible = (GameBoard.MaxX - 1) * (GameBoard.MaxY - 1);

            if (totalPossible == positionsToAvoidFoodPlacement.Length)
            {
                GameWon?.Invoke();
            }

            var foodPosition = _positionGenerator.GetUniqueRandomPosition(GameBoard.MaxX - 1, GameBoard.MaxY - 1, positionsToAvoidFoodPlacement);

            _food = new Food(foodPosition);
        }

        internal void MoveSnake(PositionTransform transform)
        {
            _snake.Move(transform);
        }

        public void CheckGameBoardCollision()
        {
            var collisionCondition = 
                _snake.Position.XPosition == GameBoard.MinX
                ||
                _snake.Position.XPosition == GameBoard.MaxX
                ||
                _snake.Position.YPosition == GameBoard.MinY
                ||
                _snake.Position.YPosition == GameBoard.MaxY;

            if (collisionCondition) GameLost?.Invoke();
        }

        internal void IncrementMoves()
        {
            Score.IncrementMoves();
        }

        internal void DetectFoodCollision()
        {
            if(_snake.Position == _food?.Position)
            {
                FeedSnake();
                CreateFood();
                FoodEaten?.Invoke();
            }
        }
    }            
}
