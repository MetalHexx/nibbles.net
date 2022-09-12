using Nibbles.GameObject;

namespace Nibbles.Engine
{
    public class GameState
    {  
        public event Action? GameOver, FoodEaten;                      
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

        private GameTextBox _gameOverText;

        public Score Score { get; private set; } = new Score(
            new Position(SpriteConfig.GAME_TITLE.Length + 1, 0), "");

        public GameState()
        {
            _gameOverText = new GameTextBox("Test",
                new BorderBoxDimensions(
                    _board.Dimensions.MaxX / 2 - 15,
                    _board.Dimensions.MaxX / 2 + 15,
                    _board.Dimensions.MaxY / 2 - 5,
                    _board.Dimensions.MaxY - 7));

            _spritesToRender.Add(_board.GetSprites());            
            _spritesToRender.Add(_snake.GetSprites());
            _spritesToRender.Add(GameTitle.GetSprites());
            _spritesToRender.Add(Score.GetSprites());
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
            _spritesToRender.Add(Score.GetSprites());
        }

        public void CreateFood()
        {
            var positionsToAvoidFoodPlacement = _snake
                        .GetSprites()
                        .Select(sp => sp.GetPosition())
                        .ToArray();

            var totalPossible = (_board.Dimensions.MaxX - 1) * (_board.Dimensions.MaxY - 1);

            if (totalPossible == positionsToAvoidFoodPlacement.Length)
            {
                HandleGameOver(SpriteConfig.GAME_WIN);                
            }

            var foodPosition = _positionGenerator.GetUniqueRandomPosition(_board.Dimensions.MaxX - 1, _board.Dimensions.MaxY - 1, positionsToAvoidFoodPlacement);

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
                _snake.GetPosition().XPosition == _board.Dimensions.MinX
                ||
                _snake.GetPosition().XPosition == _board.Dimensions.MaxX
                ||
                _snake.GetPosition().YPosition == _board.Dimensions.MinY
                ||
                _snake.GetPosition().YPosition == _board.Dimensions.MaxY;

            if (collisionCondition)
            {
                HandleGameOver(SpriteConfig.GAME_LOSE);
            }

        }

        private void HandleGameOver(string text)
        {
            _gameOverText.SetText(text);
            _spritesToRender.Add(_gameOverText.GetSprites());
            GameOver?.Invoke();
        }

        internal void IncrementMoveScore()
        {
            Score.IncrementMoves();
            _spritesToRender.Add(Score.GetSprites());
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
            HandleGameOver(SpriteConfig.GAME_LOSE);
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
