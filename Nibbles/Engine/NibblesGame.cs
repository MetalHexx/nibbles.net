using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.Food;
using Nibbles.GameObject.Projectiles;
using System.Drawing;

namespace Nibbles.Engine
{
    public class NibblesGame : Game
    {
        private readonly ICollisionDetector _collisionDetector;
        private readonly GameState _state;

        public NibblesGame(GameState state, ISpriteRenderer renderer, ICollisionDetector collisionDetector) : base(renderer)
        {
            _state = state;
            _collisionDetector = collisionDetector;
            InitializeSprites();
        }

        protected override void InitializeSprites()
        {
            _renderer.AddRange(_state.Board.GetSprites());
            _renderer.AddRange(_state.Snake.GetSprites());
            _renderer.AddRange(_state.GameTitle.GetSprites());
            _renderer.AddRange(_state.Score.GetSprites());

            _collisionDetector.SnakeSelfCollision += () => HandleGameOver(SpriteConfig.GAME_LOSE);
            _collisionDetector.SnakeVenomCollison += () => HandleGameOver(SpriteConfig.GAME_LOSE);
            _collisionDetector.SnakeBoardCollison += () => HandleGameOver(SpriteConfig.GAME_LOSE);
            _collisionDetector.VenomBoardCollision += () => HandleGameOver(SpriteConfig.GAME_LOSE);
            _collisionDetector.VenomFoodCollision += () => HandleGameOver(SpriteConfig.GAME_LOSE);
            _collisionDetector.SnakeFoodCollision += OnSnakeCollisionFood;

            RegisterSpriteEvents(_state.Snake);
            RegisterSpriteEvents(_state.GameOverTextBox);
            RegisterSpriteEvents(_state.Score);

            CreateFood();
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
            CreateFood();
        }

        private void CreateFood()
        {
            var position = PositionGenerator.GetRandomPosition(
                new AbsolutePosition(new Point(0, 0), new Size(
                    SpriteConfig.BoardSizeX,
                    SpriteConfig.BoardSizeY)),
                _state.GetUnavailableFoodPositions());

            _state.Food = new FoodSprite(position);
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
