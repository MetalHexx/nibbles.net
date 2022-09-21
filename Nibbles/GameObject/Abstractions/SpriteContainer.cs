﻿using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.Abstractions
{
    public abstract class SpriteContainer : ISpriteContainer
    {
        public Action<ISprite>? SpriteDestroyed { get; set; }
        public Action<ISprite>? SpriteCreated { get; set; }        
        public DirectionType Direction { get; protected set; } = DirectionType.None;
        public double VelocityX { get; private set; } = GameConfig.SPRITE_DEFAULT_VELOCITY_X;
        public double VelocityY { get; private set; } = GameConfig.SPRITE_DEFAULT_VELOCITY_Y;
        public GameColor ForegroundColor { get; protected set; }
        public GameColor BackgroundColor { get; protected set; }
        public char DisplayCharacter { get; protected set; } = ' ';        

        protected Point _position;
        public Point Position
        {
            get => _position with { }; protected set => _position = value;
        }
        private TimeSpan _timeSinceMove = new TimeSpan();

        protected readonly List<ISprite> _sprites = new();
        

        public SpriteContainer(Point position, DirectionType direction, GameColor foregroundColor, GameColor backgroundColor)
        {
            Position = position;
            Direction = direction;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public SpriteContainer(Point position, DirectionType direction, GameColor foregroundColor, GameColor backgroundColor, double velocityX, double velocityY)
        {   
            Position = position;
            Direction = direction;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            VelocityX = velocityX;
            VelocityY = velocityY;
        }

        public IEnumerable<ISprite> GetSprites() => _sprites;

        public void Move(long timeDelta)
        {
            foreach (var sprite in _sprites)
            {
                if (!sprite.CanRender(timeDelta)) return;

                sprite.Move(timeDelta);
            }
            Position = _sprites.First().Position;
        }

        public virtual void Move(PositionTransform transform, long timeDelta)
        {   
            foreach (var sprite in _sprites)
            {
                if (!sprite.CanRender(timeDelta)) return;
                sprite.Move(transform, timeDelta);
            }
            Position = _sprites.First().Position;
        }

        public virtual bool CanRender(long timeDelta) => _sprites
            .First()
            .CanRender(timeDelta);
        

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
