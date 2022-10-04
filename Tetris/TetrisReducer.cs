using Nibbles.Engine;
using Nibbles.Engine.Abstractions;
using Nibbles.GameObject.Dimensions;

namespace Tetris
{
    internal class TetrisReducer : GameStateReducer
    {
        private TetrisState _state;
        private CollisionDetector _collisionDetector;
        private HighScoreStore _scoreStore;
        private SoundGenerator _soundGenerator;

        public TetrisReducer(TetrisState state, SpriteRenderer renderer, CollisionDetector collisionDetector, HighScoreStore scoreStore, SoundGenerator soundGenerator) : base(renderer)
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
            _renderer.Add(_state.Board);
            _renderer.Add(_state.ActiveTetrimino);

            RegisterContainerEvents(_state.GameOverTextBox);
            RegisterContainerEvents(_state.Score);
            RegisterContainerEvents(_state.Board);
            RegisterContainerEvents(_state.ActiveTetrimino);

            _collisionDetector.TetriminoSideCollision += OnTetriminoBottomCollision;
        }

        public override void GenerateFrame()
        {
            var timeSinceLastFrame = GetTimeSinceLastFrame();
            var playerState = _state.Player.NextState();
            var playerMove = playerState.GetMove();

            _state.ActiveTetrimino.Move(timeSinceLastFrame);


            if (playerState.MovingState is MovingState.MovingUp)
            {
                _state.ActiveTetrimino.Rotate(playerMove);
            }

            var canMove = (playerState.MovingState is MovingState.MovingLeft or MovingState.MovingRight or MovingState.MovingDown)
                && _collisionDetector.IsSideCollidingWithWallOrCompletedTetriminos() is false;

            if (canMove)
            {
                _state.ActiveTetrimino.InstantMove(playerState.GetMove());
                _state.ActiveTetrimino.InstantMove(new PositionTransform(0, 0, DirectionType.Down));
            }

            if (playerState.ActionState == ActionState.Shooting)
            {
                var oldTetrimino = _state.ActiveTetrimino;
                _renderer.Remove(oldTetrimino);
                _state.CreateTetrimino();                
                RegisterContainerEvents(_state.ActiveTetrimino);
                _renderer.Add(_state.ActiveTetrimino);
                return;
            }

            _collisionDetector.Detect();

            if(playerState.ActionState == ActionState.Quitting)
            {
                GameOver?.Invoke();
            }
        }

        private void OnTetriminoBottomCollision()
        {
            _state.CompletedTetriminos.Add(_state.ActiveTetrimino);
            _state.CreateTetrimino();
            RegisterContainerEvents(_state.ActiveTetrimino);
            _renderer.Add(_state.ActiveTetrimino);
        }

        protected override void HandleGameOver(string text)
        {
            throw new NotImplementedException();
        }

        protected override void HandleGameWin(string text)
        {
            throw new NotImplementedException();
        }
    }
}