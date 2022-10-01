using Nibbles.Engine;
using Nibbles.Engine.Abstractions;
using SnakesGame.GameObject;

namespace SnakesGame.Engine
{
    public class SnakesStateReducer : GameStateReducer
    {
        private readonly ICollisionDetector _collisionDetector;
        private readonly IHighScoreStore _scoreStore;
        private readonly ISoundGenerator _soundGenerator;
        private readonly SnakesState _state;

        public SnakesStateReducer(SnakesState state, ISpriteRenderer renderer, ICollisionDetector collisionDetector, IHighScoreStore scoreStore, ISoundGenerator soundGenerator) : base(renderer)
        {
            _state = state;
            _collisionDetector = collisionDetector;
            _scoreStore = scoreStore;
            _soundGenerator = soundGenerator;
            Initialize();
        }

        private void Initialize()
        {               
            _renderer.Add(_state.GameTitle);
            _renderer.Add(_state.Score);
            _renderer.Add(_state.Snake);
            _renderer.Add(_state.Food);
            _renderer.Add(_state.Board);

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

        public override void GenerateFrame()
        {
            var timeSinceLastFrame = GetTimeSinceLastFrame();
            var playerState = _state.Player.NextState();
            HandlePlayerMove(timeSinceLastFrame, playerState);
            HandlePlayerShoot(timeSinceLastFrame, playerState);
            _collisionDetector.Detect();
        }

        private void HandlePlayerMove(long timeSinceLastFrame, PlayerState playerState)
        {
            if (playerState.MovingState == MovingState.Idle)
            {
                _state.Snake.Move(playerState.GetLastNonIdleMove(), timeSinceLastFrame);
            }
            else
            {
                _state.Score.IncrementMoves();
                _state.Snake.Move(playerState.GetMove(), timeSinceLastFrame);
            }
        }

        private void HandlePlayerShoot(long timeSinceLastFrame, PlayerState playerState)
        {
            var snakeShouldShoot = playerState.ActionState == ActionState.Shooting 
                && _state.Venom is null;

            if (snakeShouldShoot)
            {
                _soundGenerator.Pew();
                _state.Venom = _state.Snake.Shoot();
                _state.Venom.SpriteDestroyed += OnSpriteDestroyed;
                _state.Venom.SpriteCreated += OnSpriteCreated;
                _state.Venom.VenomDestroyed += OnVenomDestroyed;
            }
            _state.Venom?.Move(timeSinceLastFrame);
        }

        protected override void HandleGameWin(string text)
        {
            throw new NotImplementedException(); //lol, no way to win yet
        }

        protected override void HandleGameOver(string text)
        {
            _soundGenerator.PlayGameOverSoundAsync();
            _state.GameOverTextBox.SetText(text);
            _scoreStore.SaveScore(new TopScore(SnakesConfig.GAME_ID, "HEX", _state.Score.Total));
            GameOver?.Invoke();
        }

        private void OnSnakeCollisionFood()
        {
            _state.Snake.Feed();
            _state.Score.IncrementAmountEaten();
            _state.CreateFood();
            _renderer.Add(_state.Food);
            _soundGenerator.PlayLevelUpSoundAsync();
            
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
