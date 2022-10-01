using Nibbles.Engine;
using Nibbles.Engine.Abstractions;

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

            RegisterEvents(_state.GameOverTextBox);
            RegisterEvents(_state.Score);
            RegisterEvents(_state.Board);
            RegisterEvents(_state.Tetrimino);
        }

        public override void GenerateFrame()
        {
            var timeSinceLastFrame = GetTimeSinceLastFrame();
            var playerState = _state.Player.NextState();
            _state.Tetrimino.UpdateState(playerState.GetMove().Direction);

            if(playerState.ActionState == ActionState.Shooting)
            {
                var oldTetrimino = _state.Tetrimino;
                _renderer.Remove(oldTetrimino);
                var newTetrimino = _state.CreateTetrimino();
                _renderer.Add(newTetrimino);
                RegisterEvents(newTetrimino);
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