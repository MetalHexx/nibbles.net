using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.Projectiles;

namespace Nibbles.GameObject.Snake
{
    public class SnakeContainer : SpriteContainer
    {
        public event Action? TouchedSelf;
        public event Action<ISprite>? SnakePartCreated, SnakePartDestroyed;

        private int _remainingGrowth = 0;
        private const int GROWTH_AMOUNT = 5;
        private bool _switchAltColor = false;
        private char _currentDisplayCharacter = ' ';

        public SnakeContainer() : base(new Position(5, 5), ConsoleColor.Cyan, ConsoleColor.Cyan)
        {
            Build();
        }

        protected void Build()
        {
            _sprites.Add(new SnakePart(new Position(5, 5)));
        }

        public void Feed() => _remainingGrowth += GROWTH_AMOUNT;
           
        public Venom Shoot()
        {
             return new Venom(Position);
        }

        public override void Move(PositionTransform transform, long timeDelta)
        {

            DoMove(transform, timeDelta);

            if (IsTouchingSelf) TouchedSelf?.Invoke();
        }

        private void DoMove(PositionTransform transform, long timeDelta)
        {
            var head = _sprites.First();

            if (!head.ShouldMove(transform, timeDelta)) return;

            var newHead = new SnakePart(
                head.GetPosition(),
                 GetColor());
            
            newHead.Move(transform);
            _sprites.Insert(0, newHead);
            Grow();
            SnakePartCreated?.Invoke(newHead);
            Position = _sprites.First().GetPosition();
        }

        private ConsoleColor GetColor()
        {
            SwitchColor();
            return _switchAltColor
                ? SpriteConfig.SNAKE_BACKGROUND_ALT_COLOR
                : SpriteConfig.SNAKE_BACKGROUND_COLOR;
        }

        private char GetDisplayCharacter() => _switchAltColor ? '=' : ' ';

        private void SwitchColor()
        {
            _switchAltColor = _sprites.Count > 1
                ? !_switchAltColor
                : _switchAltColor;
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
