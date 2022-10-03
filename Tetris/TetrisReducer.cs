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
            _renderer.Add(_state.Tetrimino);

            RegisterContainerEvents(_state.GameOverTextBox);
            RegisterContainerEvents(_state.Score);
            RegisterContainerEvents(_state.Board);
            RegisterContainerEvents(_state.Tetrimino);
        }

        public override void GenerateFrame()
        {
            var timeSinceLastFrame = GetTimeSinceLastFrame();
            var playerState = _state.Player.NextState();
            var playerMove = playerState.GetMove();

            _state.Tetrimino.Move(timeSinceLastFrame);


            if (playerState.MovingState is MovingState.MovingUp)
            {
                _state.Tetrimino.Rotate(playerMove);
            }
            if (playerState.MovingState is MovingState.MovingLeft or MovingState.MovingRight or MovingState.MovingDown)
            {
                _state.Tetrimino.InstantMove(playerState.GetMove());
                _state.Tetrimino.InstantMove(new PositionTransform(0, 0, DirectionType.Down));
            }

            if (playerState.ActionState == ActionState.Shooting)
            {
                var oldTetrimino = _state.Tetrimino;
                _renderer.Remove(oldTetrimino);
                _state.CreateTetrimino();                
                RegisterContainerEvents(_state.Tetrimino);
                _renderer.Add(_state.Tetrimino);
                return;
            }
            if(playerState.ActionState == ActionState.Quitting)
            {
                GameOver?.Invoke();
            }
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