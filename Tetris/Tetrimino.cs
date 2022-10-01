using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Tetris
{

    public abstract class Tetrimino : SpriteContainer
    {
        protected GameColor _color = GameColor.Red;
        protected static readonly Point _startingPosition = new(60, 5);
        protected RotationState _rotationState = RotationState.Up;
        protected RotationState _previousState = RotationState.None;

        public Tetrimino(GameColor color): base(_startingPosition, 1, DirectionType.Down, GameColor.Cyan, GameColor.Cyan, 1, 1)
        {
            _color = color;
        }

        protected abstract int[,] GetRotation(RotationState state);

        public void UpdateRotation(DirectionType inputDirection)
        {
            _rotationState = (_rotationState, inputDirection) switch
            {
                (RotationState.Up, DirectionType.Up) => RotationState.Right,
                (RotationState.Right, DirectionType.Up) => RotationState.Down,
                (RotationState.Down, DirectionType.Up) => RotationState.Left,
                (RotationState.Left, DirectionType.Up) => RotationState.Up,
                _ => _rotationState
            };

            if (_previousState == _rotationState) return;

            _previousState = _rotationState;
            Rotate();
        }

        private void Rotate()
        {
            Clear();
            var rotationMatrix = GetRotation(_rotationState);

            for (int x = 0; x < rotationMatrix.GetLength(0); x++)
            {
                for (int y = 0; y < rotationMatrix.GetLength(1); y++)
                {
                    var shouldCreate = rotationMatrix[x, y];

                    if (shouldCreate == 1)
                    {
                        Add(new TetriminoPart(new Point(x + Position.X, y + Position.Y), _color));
                    }
                }
            }
        }
    }
}
