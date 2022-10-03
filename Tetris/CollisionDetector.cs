using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.UI;

namespace Tetris
{
    internal class CollisionDetector
    {
        public Action? TetriminoCollision { get; set; }

        private TetrisState _state;

        public CollisionDetector(TetrisState gameState)
        {
            _state = gameState;
        }

        public void Detect()
        {
            var boarderParts = _state.Board
                .GetSprites()
                .Where(boardPart => boardPart is BorderPart);

            DetectTetriminoCollision(
                boarderParts, 
                _state.ActiveTetrimino.GetSprites());

            DetectTetriminoCollision(
                _state.CompletedTetriminos.SelectMany(tetrimino => tetrimino.GetSprites()), 
                _state.ActiveTetrimino.GetSprites());
        }

        private void DetectTetriminoCollision(IEnumerable<ISprite> sprites1, IEnumerable<ISprite> sprites2)
        {
            var collisionsDetected = sprites1.Any(completed =>
                sprites2.Any(active => 
                    completed.Position.X == active.Position.X 
                    && completed.Position.Y == active.Position.Y + 1));

            if (collisionsDetected)
            {
                TetriminoCollision?.Invoke();
            }
        }
    }
}