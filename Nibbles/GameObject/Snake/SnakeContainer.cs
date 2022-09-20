using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using Nibbles.GameObject.Projectiles;
using System.Drawing;

namespace Nibbles.GameObject.Snake
{
    public class SnakeContainer : SpriteContainer
    {
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
            Add(new SnakeSprite(
                new Point(
                    Position.X, 
                    Position.Y), 
                Direction));
        }

        public void Feed() => _remainingGrowth += SpriteConfig.SNAKE_GROWTH_PER_FEED;
           
        public Venom Shoot()
        {
            var venom = new Venom(Position, Direction);
             SpriteCreated?.Invoke(venom);
            return venom;
        }

        public override void Move(PositionTransform transform, long timeDelta)
        {
            if (!CanRender(timeDelta)) return;

            DoMove(transform, timeDelta);
        }

        private void DoMove(PositionTransform transform, long timeDelta)
        {
            var head = _sprites.First();

            Direction = transform.Direction;

            var newHead = new SnakeSprite(
                head.Position,
                Direction,
                GetColor());
            
            newHead.Move(transform, timeDelta);
            Position = newHead.Position;

            Insert(newHead);
            
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
            Remove(partToRemove);
        }
    }
}
