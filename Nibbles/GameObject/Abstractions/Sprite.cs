using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.Abstractions
{
    public abstract record Sprite : ISprite
    {
        public Action<ISprite>? SpriteDestroyed, SpriteCreated;
        public Point Position { get; protected set; }
        protected DirectionType Direction { get; private set; } = DirectionType.None;
        public GameColor ForegroundColor { get; protected set; }
        public GameColor BackgroundColor { get; protected set; }
        public char DisplayCharacter { get; protected set; } = ' ';
        private TimeSpan _timeSinceMove = new TimeSpan();
        private double _velocityX = SpriteConfig.DEFAULT_SPRITE_VELOCITY_X;
        private double _velocityY = SpriteConfig.DEFAULT_SPRITE_VELOCITY_Y;
        

        public Sprite(Point position, DirectionType direction, GameColor foregroundColor, GameColor backgroundColor, char displayCharacter)
        {
            Position = position;
            Direction = direction;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            DisplayCharacter = displayCharacter;
        }

        public Sprite(Point position, DirectionType direction, GameColor foregroundColor, GameColor backgroundColor, char displayCharacter, double velocityX, double velocityY)
        {
            Position = position;
            Direction = direction;
            _velocityX = velocityX;
            _velocityY = velocityY;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            DisplayCharacter = displayCharacter;
        }

        public Point GetPosition()
        {
            return Position with { };
        }

        public virtual void Move(long timeDelta)
        {
            SpriteDestroyed?.Invoke(this with { });

            var transform = Direction switch
            {
                DirectionType.Up => new PositionTransform(0, -1, Direction),
                DirectionType.Down => new PositionTransform(0, 1, Direction),
                DirectionType.Left => new PositionTransform(-1, 0, Direction),
                DirectionType.Right => new PositionTransform(1, 0, Direction),
                _ => new PositionTransform(1, 0, Direction),
            };

            Move(transform, timeDelta);
            SpriteCreated?.Invoke(this with { });
        }

        public virtual void Move(PositionTransform transform, long timeDelta)
        {
            Direction = transform.Direction;
            Position = Position with
            {
                X = Position.X + transform.XDelta,
                Y = Position.Y + transform.YDelta
            };
        }

        /// <summary>
        /// Determines if the move is renderable based on the units velocity.
        /// Units with a higher velocity will re-render and move more often.
        /// </summary>
        public virtual bool ShouldMove(long timeDelta)
        {
            var timeSpan = new TimeSpan(timeDelta);
            _timeSinceMove += timeSpan;

            var msToWait = SpriteConfig.MIN_FRAME_RENDER_SPEED_MS / GetVelocity();

            var shouldMove = _timeSinceMove.TotalMilliseconds >= msToWait;

            if (shouldMove)
            {
                _timeSinceMove = new TimeSpan();
                return true;
            }
            return false;
        }

        public double GetVelocity()
        {
            return Direction switch
            {
                DirectionType.Up => _velocityY,
                DirectionType.Down => _velocityY,
                DirectionType.Left => _velocityX,
                DirectionType.Right => _velocityX,
                _ => _velocityX
            };
        }
    }
}
