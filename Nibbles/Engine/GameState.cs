using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.Food;
using Nibbles.GameObject.Snake;
using Nibbles.GameObject.UI;

namespace Nibbles.Engine
{
    public class GameState
    {  
        public event Action? GameOver, FoodEaten;
        private SpriteRenderUpdate _spritesToRender = new();
        private FoodSprite? _food;
        private readonly PositionGenerator _positionGenerator = new();
        private readonly Board _board = new(new Position(0, 0), new Size(100, 20));
        private readonly SnakeContainer _snake = new();
        private readonly GameTextBox _gameOverText;
        private readonly Score _score = new(
                    new Position(SpriteConfig.GAME_TITLE.Length + 1, 0), "");

        private readonly GameText GameTitle = new(
            new Position(1, 0), 
            SpriteConfig.GAME_TITLE, 
            SpriteConfig.BOARD_BORDER_FOREGROUND_COLOR, 
            SpriteConfig.BOARD_BORDER_BACKGROUND_COLOR);

        

        public GameState()
        {
            _gameOverText = new GameTextBox("", new Position(_board.Size.Width / 2 - 8, _board.Size.Height / 2 - 2), new Size(16, 4));

            _spritesToRender.Add(_board.GetSprites());            
            _spritesToRender.Add(_snake.GetSprites());
            _spritesToRender.Add(GameTitle.GetSprites());
            _spritesToRender.Add(_score.GetSprites());
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
            _score.IncrementAmountEaten();
            _spritesToRender.Add(_score.GetSprites());
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

            _food = new FoodSprite(foodPosition);
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
            _score.IncrementMoves();
            _spritesToRender.Add(_score.GetSprites());
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
