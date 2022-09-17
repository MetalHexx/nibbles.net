using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Abstractions
{
    public abstract record Sprite : ISprite
    {
        public Position _position { get; protected set; }
        public ConsoleColor ForegroundColor { get; protected set; }
        public ConsoleColor BackgroundColor { get; protected set; }
        public char DisplayCharacter { get; protected set; } = ' ';
        private TimeSpan _timeSinceMove = new TimeSpan();
        private long _velocity = 1;
        private const int MIN_RENDER_DELAY_MS = 200;
        private PositionTransform _lastMove = new PositionTransform(0, 0, DirectionType.NoChange);

        public Sprite(Position position, ConsoleColor foregroundColor, ConsoleColor backgroundColor, char displayCharacter)
        {
            _position = position;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            DisplayCharacter = displayCharacter;
        }

        public Sprite(Position position, ConsoleColor foregroundColor, ConsoleColor backgroundColor, char displayCharacter, long velocity)
        {
            _position = position;
            _velocity = velocity;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            DisplayCharacter = displayCharacter;
        }

        public Position GetPosition()
        {
            return _position with { };
        }

        public virtual void Move(PositionTransform transform, long timeDelta = 0)
        {
            _position = _position with
            {
                XPosition = _position.XPosition + transform.XDelta,
                YPosition = _position.YPosition + transform.YDelta
            };
        }

        public virtual bool ShouldMove(PositionTransform transform, long timeDelta)
        {
            var timeSpan = new TimeSpan(timeDelta);
            _timeSinceMove += timeSpan;

            var shouldWaitToRender = _timeSinceMove.TotalMilliseconds < MIN_RENDER_DELAY_MS / _velocity;

            if (shouldWaitToRender) return false;

            _timeSinceMove = new TimeSpan();
            return true;
        }

    }
}
