using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace SnakesGame.GameObject
{
    public class SnakeContainer : SpriteContainer
    {
        private int _remainingGrowth = 0;
        private bool _switchAltColor = false;

        public SnakeContainer(): base(
                new Point(
                    SnakesConfig.SNAKE_STARTING_POSITION_X, 
                    SnakesConfig.SNAKE_STARTING_POSITION_Y),
                SnakesConfig.SNAKE_ZINDEX,
                DirectionType.Right,
                GameColor.Cyan,
                GameColor.Cyan,
                SnakesConfig.DEFAULT_SPRITE_VELOCITY_X, SnakesConfig.DEFAULT_SPRITE_VELOCITY_Y)
        {
            Build();
        }

        public void Feed() => _remainingGrowth += SnakesConfig.SNAKE_GROWTH_PER_FEED;

        public Venom Shoot()
        {
            var venom = new Venom(Position, Direction);
            SpriteCreated?.Invoke(venom);
            return venom;
        }

        public override void Move(PositionTransform move, long timeDelta)
        {
            EnqueueIfChanged(move);

            if (!CanRender(timeDelta)) return;

            Direction = move.Direction;
            var head = _sprites.First();

            var newHead = new SnakePart(
                head.Position,
                Direction,
                GetColor());

            ExecuteMove(move, timeDelta, newHead);
            Position = newHead.Position;
            Insert(newHead);
            MaybeGrow();
        }

        private void ExecuteMove(PositionTransform move, long timeDelta, SnakePart newHead)
        {
            if (_moveQueue.Count > 0)
            {
                ExecuteQueuedMoves(timeDelta, newHead);
                return;
            }
            newHead.Move(move, timeDelta);            
        }

        private void EnqueueIfChanged(PositionTransform move)
        {
            if (move != _lastMove)
            {
                _moveQueue.Enqueue(move);
            }
            _lastMove = move;
        }

        private GameColor GetColor()
        {
            SwitchColor();
            return _switchAltColor
                ? SnakesConfig.SNAKE_BACKGROUND_ALT_COLOR
                : SnakesConfig.SNAKE_BACKGROUND_COLOR;
        }

        private void SwitchColor()
        {
            _switchAltColor = _sprites.Count > 1
                ? !_switchAltColor
                : _switchAltColor;
        }

        private void MaybeGrow()
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
