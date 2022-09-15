using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Snake
{
    public class SnakeContainer : SpriteContainer
    {
        public event Action? TouchedSelf;
        public event Action<ISprite>? SnakePartCreated, SnakePartDestroyed;

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
            DoMove(transform);

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
