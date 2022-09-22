using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Dimensions;
using SnakesGame.GameObject;

namespace SnakesGame.Engine
{
    public class SnakesManager : GameManager
    {
        private readonly ICollisionDetector _collisionDetector;
        private readonly GameState _state;

        public SnakesManager(GameState state, ISpriteRenderer renderer, ICollisionDetector collisionDetector) : base(renderer)
        {
            _state = state;
            _collisionDetector = collisionDetector;
            InitializeSprites();
        }

        protected override void InitializeSprites()
        {            
            _renderer.Add(_state.Board);            
            _renderer.Add(_state.GameTitle);
            _renderer.Add(_state.Score);
            _renderer.Add(_state.Snake);
            _renderer.Add(_state.Food);

            _collisionDetector.SnakeSelfCollision += () => HandleGameOver(SnakesConfig.GAME_LOSE);
            _collisionDetector.SnakeVenomCollison += () => HandleGameOver(SnakesConfig.GAME_LOSE);
            _collisionDetector.SnakeBoardCollison += () => HandleGameOver(SnakesConfig.GAME_LOSE);
            _collisionDetector.VenomBoardCollision += () => HandleGameOver(SnakesConfig.GAME_LOSE);
            _collisionDetector.VenomFoodCollision += () => HandleGameOver(SnakesConfig.GAME_LOSE);
            _collisionDetector.SnakeFoodCollision += OnSnakeCollisionFood;

            RegisterEvents(_state.Snake);
            RegisterEvents(_state.GameOverTextBox);
            RegisterEvents(_state.Score);
            RegisterEvents(_state.Board);
        }

        public override void PlayerMove()
        {
            _state.Score.IncrementMoves();
        }

        public override void PlayerShoot()
        {
            if (_state.Venom != null) return;

            _state.Venom = _state.Snake.Shoot();
            _state.Venom.SpriteDestroyed += OnSpriteDestroyed;
            _state.Venom.SpriteCreated += OnSpriteCreated;
            _state.Venom.VenomDestroyed += OnVenomDestroyed;
        }

        public override void UpdateState(PositionTransform playerInput, long timeDelta)
        {
            _collisionDetector.Detect();
            _state.Snake.Move(playerInput, timeDelta);
            _state.Venom?.Move(timeDelta);
        }

        protected override void HandleGameWin(string text)
        {
            throw new NotImplementedException(); //lol, no way to win yet
        }

        protected override void HandleGameOver(string text)
        {
            _state.GameOverTextBox.SetText(text);
            GameOver?.Invoke();
        }

        private void OnSnakeCollisionFood()
        {
            _state.Snake.Feed();
            _state.Score.IncrementAmountEaten();
            _state.CreateFood();
            _renderer.Add(_state.Food);            
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
    }
}
