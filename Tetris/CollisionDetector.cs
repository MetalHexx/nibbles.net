using Nibbles.Engine;
using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.UI;

namespace Tetris
{
    internal class CollisionDetector
    {
        public Action? TetriminoBottomCollision { get; set; }
        public Action? TetriminoSideCollision { get; set; }

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

            var bottomBorderParts = boarderParts.Where(boardPart => 
                boardPart.Position.Y == _state.Board.Dimensions.MaxY);

            DetectTetriminoBottomCollision(
                bottomBorderParts, 
                _state.ActiveTetrimino.GetSprites());

            DetectTetriminoBottomCollision(
                _state.CompletedTetriminos.SelectMany(tetrimino => tetrimino.GetSprites()), 
                _state.ActiveTetrimino.GetSprites());
        }

        private void DetectTetriminoBottomCollision(IEnumerable<ISprite> sprites1, IEnumerable<ISprite> sprites2)
        {
            var collisionsDetected = sprites1.Any(completed =>
                sprites2.Any(active => 
                    completed.Position.X == active.Position.X 
                    && completed.Position.Y == active.Position.Y + 1));

            if (collisionsDetected)
            {
                TetriminoSideCollision?.Invoke();
            }
        }

        /// <summary>
        /// Checks to see if the player is adjacent to and headed toward a side wall or the side of the completed tetriminos
        /// </summary>
        /// <remarks>Checking the direction is crutial otherwise the player can get stuck to an adjacent sprite</remarks>
        public bool IsSideCollidingWithWallOrCompletedTetriminos()
        {
            var activeTetrimino = _state.ActiveTetrimino.GetSprites();

            if (_state.Player.MovingState is MovingState.MovingRight)
            {
                var rightBorderAndTetriminos = _state.Board.GetSprites()
                    .Where(boardPart => boardPart is BorderPart
                        && boardPart.Position.X == _state.Board.Dimensions.MaxX)
                    .Concat(_state.CompletedTetriminos.SelectMany(completed => completed.GetSprites()));

                return rightBorderAndTetriminos.Any(borderPart =>
                    activeTetrimino.Any(active =>
                        borderPart.Position.Y == active.Position.Y
                        && (borderPart.Position.X == active.Position.X + 1)));
            }
            if (_state.Player.MovingState is MovingState.MovingLeft)
            {
                var leftBorderPartsAndCompletedTetriminos = _state.Board.GetSprites()
                    .Where(boardPart => boardPart is BorderPart
                        && boardPart.Position.X == _state.Board.Dimensions.MinX)
                    .Concat(_state.CompletedTetriminos.SelectMany(completed => completed.GetSprites()));

                return leftBorderPartsAndCompletedTetriminos.Any(borderPart =>
                    activeTetrimino.Any(active =>
                        borderPart.Position.Y == active.Position.Y
                        && (borderPart.Position.X == active.Position.X - 1)));
            }
            return false;            
        }
    }
}