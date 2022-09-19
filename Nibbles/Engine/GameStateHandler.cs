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
        public event Action? GameOver;
        private ISpriteRenderer _renderer;
        private readonly ICollisionDetector _collisionDetector;
        private readonly PositionGenerator _positionGenerator = new();
        private readonly GameState _state;

        public GameStateHandler(GameState state, ISpriteRenderer renderer, ICollisionDetector collisionDetector)
        {
            _state = state;
            _renderer = renderer;
            _collisionDetector = collisionDetector;
            InitializeSprites();
        }

        private void InitializeSprites()
        {
            _renderer.AddRange(_state.Board.GetSprites());
            _renderer.AddRange(_state.Snake.GetSprites());
            _renderer.AddRange(_state.GameTitle.GetSprites());
            _renderer.AddRange(_state.Score.GetSprites());

            _state.Snake.TouchedSelf += OnSnakeTouchedSelf;

            RegisterSpriteEvents(_state.Snake);
            RegisterSpriteEvents(_state.GameOverTextBox);
            RegisterSpriteEvents(_state.Score);

            CreateFood();
        }

        public void CreateFood()
        {
            var positionsToAvoidFoodPlacement = _state.Snake
                .GetSprites()
                .Select(sp => sp.Position)
                .ToArray();

            var foodPosition = _positionGenerator.GetUniqueRandomPosition(_state.Board.Dimensions.MaxX - 1, _state.Board.Dimensions.MaxY - 1, positionsToAvoidFoodPlacement);

            _state.Food = new FoodSprite(foodPosition);

            _collisionDetector.Register(_state.Snake, _state.Food, 
                _state.Snake.Feed, 
                _state.Score.IncrementAmountEaten,
                CreateFood);
            
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
                _state.Snake.Position.X == _state.Board.Dimensions.MinX
                ||
                _state.Snake.Position.X == _state.Board.Dimensions.MaxX
                ||
                _state.Snake.Position.Y == _state.Board.Dimensions.MinY
                ||
                _state.Snake.Position.Y == _state.Board.Dimensions.MaxY;

            if (collisionCondition)
            {
                HandleGameOver(SpriteConfig.GAME_LOSE);
            }

        }

        private void HandleGameOver(string text)
        {
            _state.GameOverTextBox.SetText(text);
            GameOver?.Invoke();
        }

        public void IncrementMoveScore()
        {
            _state.Score.IncrementMoves();
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

        private void RegisterSpriteEvents(ISprite sprite)
        {
            sprite.SpriteCreated += OnSpriteCreated;
            sprite.SpriteDestroyed += OnSpriteDestroyed;
        }
        private void UnregisterSpriteEvents(ISprite sprite)
        {
            sprite.SpriteCreated -= OnSpriteCreated;
            sprite.SpriteDestroyed -= OnSpriteDestroyed;
        }
    }
}
