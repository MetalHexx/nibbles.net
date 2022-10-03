using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Tetris.GameObjects
{

    public abstract class Tetrimino : SpriteContainer
    {
        protected GameColor _color = GameColor.Red;
        protected static readonly Point _startingPosition = new(60, 5);
        protected RotationState _rotationState = RotationState.Up;
        protected RotationState _previousState = RotationState.None;

        public Tetrimino(GameColor color): base(_startingPosition, 1, DirectionType.Down, GameColor.Cyan, GameColor.Cyan, 0.2, 0.2)
        {
            _color = color;
            Rotate();
        }

        protected abstract int[,] GetRotation(RotationState state);

        public override void InstantMove(PositionTransform transform)
        {
            var spritesToRemove = new List<ISprite>();
            var spritesToAdd = new List<ISprite>();

            foreach (TetriminoPart sprite in _sprites)
            {
                var newSprite = sprite with { };
                spritesToRemove.Add(sprite);
                newSprite.InstantMove(transform);
                spritesToAdd.Add(newSprite);
            }
            RemoveRange(spritesToRemove);
            AddRange(spritesToAdd);
            Position = _position with
            {
                X = _position.X + transform.XDelta,
                Y = _position.Y + transform.YDelta
            };
            SpriteContainerChanged?.Invoke(this);
        }

        public void Rotate(PositionTransform transform)
        {   
            _rotationState = (_rotationState, transform.Direction) switch
            {
                (RotationState.Up, DirectionType.Up) => RotationState.Left,
                (RotationState.Left, DirectionType.Up) => RotationState.Down,
                (RotationState.Down, DirectionType.Up) => RotationState.Right,
                (RotationState.Right, DirectionType.Up) => RotationState.Up,
                _ => _rotationState
            };

            if (_previousState == _rotationState) return;
            
            _previousState = _rotationState;
            Rotate();
            SpriteContainerChanged?.Invoke(this);
        }

        private void Rotate()
        {
            var sprite = _sprites.FirstOrDefault();
            
            var timeSinceMove = sprite is null 
                ? new TimeSpan() 
                : sprite.TimeSinceMove;

            Clear();
            var rotationMatrix = GetRotation(_rotationState);

            for (int x = 0; x < rotationMatrix.GetLength(0); x++)
            {
                for (int y = 0; y < rotationMatrix.GetLength(1); y++)
                {
                    var shouldCreate = rotationMatrix[x, y];

                    if (shouldCreate == 1)
                    {
                        Add(new TetriminoPart(new Point(x + Position.X, y + Position.Y), _color, VelocityY, timeSinceMove));
                    }
                }
            }
        }
    }
}
