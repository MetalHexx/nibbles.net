using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.Food;
using Nibbles.GameObject.Projectiles;

namespace Nibbles.Engine
{
    public class GameStateHandler : IGameStateHandler
    {
        public event Action? GameOver, FoodEaten;
        //private SpriteRenderUpdate _spritesToRender = new();
        private ISpriteRenderer _renderer;
        private readonly PositionGenerator _positionGenerator = new();
        private readonly GameState _state;

        public GameStateHandler(GameState state, ISpriteRenderer renderer)
        {
            _state = state;
            _renderer = renderer;
            InitializeSprites();
        }

        private void InitializeSprites()
        {
            _renderer.AddRange(_state.Board.GetSprites());
            _renderer.AddRange(_state.Snake.GetSprites());
            _renderer.AddRange(_state.GameTitle.GetSprites());
            _renderer.AddRange(_state.Score.GetSprites());

            _state.Snake.TouchedSelf += OnSnakeTouchedSelf;
            _state.Snake.SnakePartCreated += OnSpriteCreated;
            _state.Snake.SnakePartDestroyed += OnSpriteDestroyed;
            CreateFood();
        }

        public void FeedSnake()
        {
            _state.Snake.Feed();
            _state.Score.IncrementAmountEaten();
            _renderer.AddRange(_state.Score.GetSprites());
        }

        public void CreateFood()
        {
            var positionsToAvoidFoodPlacement = _state.Snake
                .GetSprites()
                .Select(sp => sp.GetPosition())
                .ToArray();

            var totalNumPositions = (_state.Board.Dimensions.MaxX - 1) * (_state.Board.Dimensions.MaxY - 1);

            if (totalNumPositions == positionsToAvoidFoodPlacement.Length)
            {
                HandleGameOver(SpriteConfig.GAME_WIN);
            }

            var foodPosition = _positionGenerator.GetUniqueRandomPosition(_state.Board.Dimensions.MaxX - 1, _state.Board.Dimensions.MaxY - 1, positionsToAvoidFoodPlacement);

            _state.Food = new FoodSprite(foodPosition);
            _renderer.Add(_state.Food);
        }

        public void UpdateSprites(PositionTransform playerInput, long timeDelta)
        {
            _state.Snake.Move(playerInput, timeDelta);
            _state.Venom?.Move(timeDelta);
        }

        public void SnakeShoot()
        {
            if (_state.Venom != null) return;

            _state.Venom = _state.Snake.Shoot();
            _state.Venom.SpriteDestroyed += OnSpriteDestroyed;
            _state.Venom.SpriteCreated += OnSpriteCreated;
            _state.Venom.VenomDestroyed += OnVenomDestroyed;
        }

        public void CheckGameBoardCollision()
        {
            var collisionCondition =
                _state.Snake.GetPosition().X == _state.Board.Dimensions.MinX
                ||
                _state.Snake.GetPosition().X == _state.Board.Dimensions.MaxX
                ||
                _state.Snake.GetPosition().Y == _state.Board.Dimensions.MinY
                ||
                _state.Snake.GetPosition().Y == _state.Board.Dimensions.MaxY;

            if (collisionCondition)
            {
                HandleGameOver(SpriteConfig.GAME_LOSE);
            }

        }

        private void HandleGameOver(string text)
        {
            _state.GameOverText.SetText(text);
            _renderer.AddRange(_state.GameOverText.GetSprites());
            GameOver?.Invoke();
        }

        public void IncrementMoveScore()
        {
            _state.Score.IncrementMoves();
            _renderer.AddRange(_state.Score.GetSprites());
        }

        public void DetectFoodCollision()
        {
            if (_state.Snake.GetPosition() == _state.Food?.GetPosition())
            {
                FeedSnake();
                CreateFood();
                FoodEaten?.Invoke();
            }
        }
        private void OnSnakeTouchedSelf()
        {
            HandleGameOver(SpriteConfig.GAME_LOSE);
        }

        private void OnVenomDestroyed(Venom venom)
        {
            _renderer.Remove(venom);

            if (_state.Venom == null) return;
            _state.Venom.SpriteDestroyed -= OnSpriteDestroyed;
            _state.Venom.SpriteCreated -= OnSpriteCreated;
            _state.Venom.VenomDestroyed -= OnVenomDestroyed;
            _state.Venom = null;
        }

        private void OnSpriteCreated(ISprite sprite)
        {
            _renderer.Add(sprite);
        }

        private void OnSpriteDestroyed(ISprite sprite)
        {
            _renderer.Remove(sprite);
        }
    }
}
