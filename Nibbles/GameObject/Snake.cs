using System;

namespace Nibbles.GameObject
{
    public class Snake : IMoveableSprite
    {
        public event Action? TouchedSelf, Moved;
        public Position Position => _snakeParts.First().Position;
        private List<SnakePart> _snakeParts = new List<SnakePart>() { new SnakePart(5, 5) };
        private PositionTransform _currentDirection = new PositionTransform(1, 0, DirectionType.Right);
        private int _remainingGrowth = 0;
        private const int GROWTH_AMOUNT = 5;

        /// <summary>
        /// Feeding the snake will change the state to track growth
        /// </summary>
        public void Feed() => _remainingGrowth = GROWTH_AMOUNT;

        /// <summary>
        /// Calculate and do the next snake move
        /// </summary>
        /// <param name="transform">Next potential snake move based on users input (or lack of input)</param>
        public void Move(PositionTransform transform)
        {
            var finalDirection = DetermineFinalDirection(transform);
            _currentDirection = finalDirection;
            DoMove(_currentDirection);            

            if (IsTouchingSelf) TouchedSelf?.Invoke();
        }

        private void DoMove(PositionTransform transform)
        {
            var head = _snakeParts.First();
            var newHead = Clone(head, transform.X, transform.Y);
            _snakeParts.Insert(0, newHead);
            MaybeGrow();
        }
        /// <summary>
        /// Prevents snake from moving in the opposite direction unless it's length is 1
        /// </summary>
        private PositionTransform DetermineFinalDirection(PositionTransform nextTransform)
        {
            if (nextTransform.Direction == DirectionType.NoChange) return _currentDirection;

            var isOnlyHead = _snakeParts.Count() == 1;

            if (isOnlyHead)
            {
                return nextTransform;
            }

            var nextDirection = nextTransform.Direction;
            var currentDirection = _currentDirection.Direction;

            var isTryingToMoveBackward =
                currentDirection == DirectionType.Up && nextDirection == DirectionType.Down
                || currentDirection == DirectionType.Down && nextDirection == DirectionType.Up
                || currentDirection == DirectionType.Left && nextDirection == DirectionType.Right
                || currentDirection == DirectionType.Right && nextDirection == DirectionType.Left;


            return isTryingToMoveBackward
                ? _currentDirection
                : nextTransform;
        }

        private void MaybeGrow()
        {
            if (_remainingGrowth > 0)
            {
                _remainingGrowth--;
            }
            else
            {
                _snakeParts.Remove(_snakeParts.Last());
            }
        }
        public IEnumerable<ISprite> GetParts()
        {
            return _snakeParts.Select(sp => Clone(sp));
        }

        public bool IsTouchingSelf => GetParts()
            .Skip(1)
            .Any(snakePart => Position == snakePart.Position);

        private SnakePart Clone(SnakePart snakePart, int xDelta = 0, int yDelta = 0)
        {
            return snakePart with
            {
                Position = snakePart.Position with
                {
                    XPosition = snakePart.Position.XPosition + xDelta,
                    YPosition = snakePart.Position.YPosition + yDelta
                }
            };
        }
    }
}
