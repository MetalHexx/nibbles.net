using Nibbles.GameObject;

namespace Nibbles.Engine
{
    public class GameState
    {  
        public event Action? GameLost, GameWon, FoodEaten;        
        public Board GameBoard { get; private set; } = new Board();
        private PositionGenerator _positionGenerator = new PositionGenerator();
        private Snake _snake;
        private Food? _food;
        private List<ISprite> _spritesToRender = new List<ISprite>();
        private List<ISprite> _spritesToDestroy = new List<ISprite>();

        public Score Score { get; private set; } = new Score();

        public GameState()
        {
            _snake = new Snake();
            _spritesToRender.Add(_snake.GetParts().First());
            _snake.TouchedSelf += OnTouchedSelf;
            _snake.SnakePartCreated += OnSpriteAdded;
            _snake.SnakePartDestroyed += OnSpriteDestroyed;
            CreateFood();
        }

        public List<ISprite> GetSpritesToDestroy()
        {
            var spritesToDestroy = _spritesToDestroy;
            _spritesToDestroy = new List<ISprite>();
            return spritesToDestroy;
        }

        public List<ISprite> GetSpritesToRender()
        {
            var spritesToRender = _spritesToRender;
            _spritesToRender = new List<ISprite>();
            return spritesToRender;
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
                        .Select(sp => sp.GetPosition())
                        .ToArray();

            var totalPossible = (GameBoard.MaxX - 1) * (GameBoard.MaxY - 1);

            if (totalPossible == positionsToAvoidFoodPlacement.Length)
            {
                GameWon?.Invoke();
            }

            var foodPosition = _positionGenerator.GetUniqueRandomPosition(GameBoard.MaxX - 1, GameBoard.MaxY - 1, positionsToAvoidFoodPlacement);

            _food = new Food(foodPosition);
            _spritesToRender.Add(_food);
        }

        internal void MoveSnake(PositionTransform transform)
        {
            _snake.Move(transform);
        }

        public void CheckGameBoardCollision()
        {
            var collisionCondition = 
                _snake.GetPosition().XPosition == GameBoard.MinX
                ||
                _snake.GetPosition().XPosition == GameBoard.MaxX
                ||
                _snake.GetPosition().YPosition == GameBoard.MinY
                ||
                _snake.GetPosition().YPosition == GameBoard.MaxY;

            if (collisionCondition) GameLost?.Invoke();
        }

        internal void IncrementMoves()
        {
            Score.IncrementMoves();
        }

        internal void DetectFoodCollision()
        {
            if(_snake.GetPosition() == _food?.GetPosition())
            {
                FeedSnake();
                CreateFood();
                FoodEaten?.Invoke();
            }
        }
        private void OnTouchedSelf()
        {
            GameLost?.Invoke();
        }

        private void OnSpriteAdded(ISprite sprite)
        {
            _spritesToRender.Add(sprite);
        }

        private void OnSpriteDestroyed(ISprite sprite)
        {
            _spritesToDestroy.Add(sprite);
        }
    }            
}
