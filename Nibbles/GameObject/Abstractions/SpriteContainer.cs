using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.Abstractions
{
    public abstract class SpriteContainer : ISpriteContainer
    {
        public Action<ISprite>? SpriteDestroyed { get; set; }

        public Action<ISprite>? SpriteCreated { get; set; }

        public Point Position { get; protected set; }
        public DirectionType Direction { get; protected set; } = DirectionType.None;
        public GameColor ForegroundColor { get; protected set; }
        public GameColor BackgroundColor { get; protected set; }
        public char DisplayCharacter { get; protected set; }

        public double VelocityX { get; private set; } = SpriteConfig.DEFAULT_SPRITE_VELOCITY_X;
        public double VelocityY { get; private set; } = SpriteConfig.DEFAULT_SPRITE_VELOCITY_Y;

        protected readonly List<ISprite> _sprites = new();
        private TimeSpan _timeSinceMove = new TimeSpan();

        public SpriteContainer(Point position, GameColor foregroundColor, GameColor backgroundColor)
        {
            Position = position;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public SpriteContainer(Point position, GameColor foregroundColor, GameColor backgroundColor, double velocityX, double velocityY)
        {   
            Position = position;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            VelocityX = velocityX;
            VelocityY = velocityY;
        }

        public IEnumerable<ISprite> GetSprites() => _sprites;

        public virtual void Move(PositionTransform transform, long timeDelta)
        {   
            foreach (var sprite in _sprites)
            {
                if (!sprite.CanRender(timeDelta)) return;
                sprite.Move(transform, timeDelta);
            }
            Position = _sprites.First().Position;
        }

        public void Move(long timeDelta)
        {
            foreach (var sprite in _sprites)
            {
                if (!sprite.CanRender(timeDelta)) return;

                sprite.Move(timeDelta);
            }
            Position = _sprites.First().Position;
        }

        public bool CanRender(long timeDelta)
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
                DirectionType.Up => VelocityY,
                DirectionType.Down => VelocityY,
                DirectionType.Left => VelocityX,
                DirectionType.Right => VelocityX,
                _ => VelocityX
            };
        }

        protected void Insert(ISprite sprite, int index = 0)
        {
            _sprites.Insert(index, sprite);
            SpriteCreated?.Invoke(sprite);
        }

        protected void Add(ISprite sprite)
        {
            _sprites.Add(sprite);
            SpriteCreated?.Invoke(sprite);
        }

        protected void AddRange(IEnumerable<ISprite> sprites)
        {
            _sprites.AddRange(sprites);
            _sprites.ForEach(sprite => SpriteCreated?.Invoke(sprite));
        }

        protected void Remove(ISprite sprite)
        {
            _sprites.Remove(sprite);
            SpriteDestroyed?.Invoke(sprite);
        }

        protected void Clear()
        {   
            _sprites.ForEach(sprite => SpriteDestroyed?.Invoke(sprite));
            _sprites.Clear();
        }
    }
}
