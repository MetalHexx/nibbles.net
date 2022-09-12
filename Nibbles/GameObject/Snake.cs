namespace Nibbles.GameObject
{
    public class Snake: ISpriteContainer
    {
        public event Action? TouchedSelf;
        public event Action<ISprite>? SnakePartCreated, SnakePartDestroyed;

        private List<SnakePart> _snakeParts = new List<SnakePart>() { new SnakePart(5, 5) };
        private PositionTransform _currentDirection = new PositionTransform(1, 0, DirectionType.Right);
        private int _remainingGrowth = 0;
        private const int GROWTH_AMOUNT = 5;

        /// <summary>
        /// Feeding the snake will change the state to track growth
        /// </summary>
        public void Feed() => _remainingGrowth += GROWTH_AMOUNT;

        public Position GetPosition()
        {
            return _snakeParts.First().GetPosition();
        }

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
            var oldHead = _snakeParts.First();
            var newHead = Copy(oldHead, transform.X, transform.Y);

            _snakeParts.Insert(0, newHead);            
            Grow();
            SnakePartCreated?.Invoke(newHead);
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

        private void Grow()
        {
            if (_remainingGrowth > 0)
            {
                _remainingGrowth--;
                return;
            }
            var partToRemove = _snakeParts.Last();
            _snakeParts.Remove(partToRemove);
            SnakePartDestroyed?.Invoke(partToRemove);
        }
        public IEnumerable<ISprite> GetSprites()
        {
            return _snakeParts.Select(sp => Copy(sp));
        }

        private bool IsTouchingSelf => GetSprites()
            .Skip(1)
            .Any(snakePart => GetPosition() == snakePart.GetPosition());

        private SnakePart Copy(SnakePart snakePart, int xDelta = 0, int yDelta = 0)
        {
            var newX = snakePart.GetPosition().XPosition + xDelta;
            var newY = snakePart.GetPosition().YPosition + yDelta;
            return new SnakePart(newX, newY);
        }
    }
}
