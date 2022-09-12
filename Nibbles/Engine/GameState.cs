using Nibbles.GameObject;

namespace Nibbles.Engine
{
    public class GameState
    {  
        public event Action? GameLost, GameWon, FoodEaten;                      
        private PositionGenerator _positionGenerator = new PositionGenerator();
        private SpriteRenderUpdate _spritesToRender = new SpriteRenderUpdate();
        private Board _board = new Board();
        private Snake _snake = new Snake();
        private Food? _food;

        private GameText GameTitle = new GameText(
            new Position(1, 0), 
            SpriteConfig.GAME_TITLE, 
            SpriteConfig.BOARD_BORDER_FOREGROUND_COLOR, 
            SpriteConfig.BOARD_BORDER_BACKGROUND_COLOR);

        public Score Score { get; private set; } = new Score(
            new Position(SpriteConfig.GAME_TITLE.Length + 1, 0), "");

        public GameState()
        {
            _spritesToRender.Add(_board.GetParts());
            _spritesToRender.Add(_snake.GetParts());
            _spritesToRender.Add(GameTitle.GetParts());
            _spritesToRender.Add(Score.GetParts());
            _snake.TouchedSelf += OnTouchedSelf;
            _snake.SnakePartCreated += OnSpriteAdded;
            _snake.SnakePartDestroyed += OnSpriteDestroyed;
            CreateFood();
        }

        public SpriteRenderUpdate GetSpritesToRender()
        {
            var spritesToRender = _spritesToRender;
            _spritesToRender = new SpriteRenderUpdate();
            return spritesToRender;
        }

        public void FeedSnake()
        {
            _snake.Feed();
            Score.IncrementAmountEaten();
            _spritesToRender.Add(Score.GetParts());
        }

        public void CreateFood()
        {
            var positionsToAvoidFoodPlacement = _snake
                        .GetParts()
                        .Select(sp => sp.GetPosition())
                        .ToArray();

            var totalPossible = (_board.MaxX - 1) * (_board.MaxY - 1);

            if (totalPossible == positionsToAvoidFoodPlacement.Length)
            {
                GameWon?.Invoke();
            }

            var foodPosition = _positionGenerator.GetUniqueRandomPosition(_board.MaxX - 1, _board.MaxY - 1, positionsToAvoidFoodPlacement);

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
                _snake.GetPosition().XPosition == _board.MinX
                ||
                _snake.GetPosition().XPosition == _board.MaxX
                ||
                _snake.GetPosition().YPosition == _board.MinY
                ||
                _snake.GetPosition().YPosition == _board.MaxY;

            if (collisionCondition) GameLost?.Invoke();
        }

        internal void IncrementMoveScore()
        {
            Score.IncrementMoves();
            _spritesToRender.Add(Score.GetParts());
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
            _spritesToRender.Remove(sprite);
        }
    }            
}
