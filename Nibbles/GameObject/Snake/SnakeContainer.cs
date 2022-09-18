using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.Projectiles;
using System.Drawing;

namespace Nibbles.GameObject.Snake
{
    public class SnakeContainer : SpriteContainer
    {
        public event Action? TouchedSelf;
        public event Action<ISprite>? SnakePartCreated, SnakePartDestroyed;

        private int _remainingGrowth = 0;
        private const int GROWTH_AMOUNT = 5;
        private bool _switchAltColor = false;

        public SnakeContainer() : base(
            new Point(
                SpriteConfig.SNAKE_STARTING_POSITION_X, 
                SpriteConfig.SNAKE_STARTING_POSITION_Y), 
            GameColor.Cyan, 
            GameColor.Cyan)
        {
            Direction = DirectionType.Right;
            Build();
        }

        protected void Build()
        {
            _sprites.Add(new SnakeSprite(
                new Point(
                    Position.X, 
                    Position.Y), 
                Direction));
        }

        public void Feed() => _remainingGrowth += GROWTH_AMOUNT;
           
        public Venom Shoot()
        {
              return new Venom(Position, Direction);
        }

        public override void Move(PositionTransform transform, long timeDelta)
        {

            DoMove(transform, timeDelta);

            if (IsTouchingSelf) TouchedSelf?.Invoke();
        }

        private void DoMove(PositionTransform transform, long timeDelta)
        {
            var head = _sprites.First();

            if (!head.ShouldMove(timeDelta)) return;

            Direction = transform.Direction;

            var newHead = new SnakeSprite(
                head.GetPosition(),
                Direction,
                GetColor());
            
            newHead.Move(transform, timeDelta);
            Position = newHead.GetPosition();

            _sprites.Insert(0, newHead);            
            SnakePartCreated?.Invoke(newHead);
            Grow();
        }

        private GameColor GetColor()
        {
            SwitchColor();
            return _switchAltColor
                ? SpriteConfig.SNAKE_BACKGROUND_ALT_COLOR
                : SpriteConfig.SNAKE_BACKGROUND_COLOR;
        }

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
