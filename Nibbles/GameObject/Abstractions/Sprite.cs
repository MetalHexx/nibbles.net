using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.Abstractions
{
    public abstract record Sprite : ISprite
    {
        public Action<ISprite>? SpriteDestroyed { get; set; }
        public Action<ISprite>? SpriteCreated { get; set; }
        protected Point _position;
        public Point Position { get { return _position with { }; } }
        public DirectionType Direction { get; private set; } = DirectionType.None;
        public double VelocityX { get; private set; } = GameConfig.DEFAULT_SPRITE_VELOCITY_X;
        public double VelocityY { get; private set; } = GameConfig.DEFAULT_SPRITE_VELOCITY_Y;
        public GameColor ForegroundColor { get; protected set; }
        public GameColor BackgroundColor { get; protected set; }
        public char DisplayCharacter { get; protected set; } = ' ';
        private TimeSpan _timeSinceMove = new TimeSpan();
        

        public Sprite(Point position, DirectionType direction, GameColor foregroundColor, GameColor backgroundColor, char displayCharacter)
        {
            _position = position;
            Direction = direction;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            DisplayCharacter = displayCharacter;
        }

        public Sprite(Point position, DirectionType direction, GameColor foregroundColor, GameColor backgroundColor, char displayCharacter, double velocityX, double velocityY)
        {
            _position = position;
            Direction = direction;            
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            DisplayCharacter = displayCharacter;
            VelocityX = velocityX;
            VelocityY = velocityY;
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
            _position = Position with
            {
                X = _position.X + transform.XDelta,
                Y = _position.Y + transform.YDelta
            };
        }

        /// <summary>
        /// Determines if the move is renderable based on the units velocity.
        /// Units with a higher velocity will re-render and move more often.
        /// </summary>
        public virtual bool CanRender(long timeDelta)
        {
            var timeSpan = new TimeSpan(timeDelta);
            _timeSinceMove += timeSpan;

            var msToWait = GameConfig.MIN_FRAME_RENDER_SPEED_MS / GetVelocity();

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
                DirectionType.Up => VelocityY,
                DirectionType.Down => VelocityY,
                DirectionType.Left => VelocityX,
                DirectionType.Right => VelocityX,
                _ => VelocityX
            };
        }
    }
}
