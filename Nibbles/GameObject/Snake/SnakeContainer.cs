using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Snake
{
    public class SnakeContainer : SpriteContainer
    {
        public event Action? TouchedSelf;
        public event Action<ISprite>? SnakePartCreated, SnakePartDestroyed;

        private PositionTransform _currentDirection = new(1, 0, DirectionType.Right);
        private int _remainingGrowth = 0;
        private const int GROWTH_AMOUNT = 5;

        public SnakeContainer() : base(new Position(5, 5), ConsoleColor.Cyan, ConsoleColor.Cyan)
        {
            Build();
        }

        protected void Build()
        {
            _sprites.Add(new SnakePart(new Position(5, 5)));
        }

        public void Feed() => _remainingGrowth += GROWTH_AMOUNT;

        public override void Move(PositionTransform transform)
        {
            var finalDirection = DetermineFinalDirection(transform);
            _currentDirection = finalDirection;
            DoMove(_currentDirection);

            if (IsTouchingSelf) TouchedSelf?.Invoke();
        }

        private void DoMove(PositionTransform transform)
        {
            var oldHead = _sprites.First();
            var newHead = new SnakePart(oldHead.GetPosition());
            newHead.Move(transform);
            _sprites.Insert(0, newHead);
            Grow();
            SnakePartCreated?.Invoke(newHead);
        }

        private PositionTransform DetermineFinalDirection(PositionTransform nextTransform)
        {
            if (nextTransform.Direction == DirectionType.NoChange) return _currentDirection;

            var isOnlyHead = _sprites.Count == 1;

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
            var partToRemove = _sprites.Last();
            _sprites.Remove(partToRemove);
            SnakePartDestroyed?.Invoke(partToRemove);
        }

        private bool IsTouchingSelf => GetSprites()
            .Skip(1)
            .Any(snakePart => GetPosition() == snakePart.GetPosition());
    }
}
