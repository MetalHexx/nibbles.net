﻿using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.Abstractions
{
    public abstract class SpriteContainer : ISpriteContainer
    {
        public Action<ISpriteContainer>? SpriteContainerChanged { get; set; }
        public Guid Id { get; } = Guid.NewGuid();
        public Action<ISprite>? SpriteDestroyed { get; set; }
        public Action<ISprite>? SpriteCreated { get; set; }
        public DirectionType Direction { get; protected set; } = DirectionType.None;
        public double VelocityX { get; private set; } = GameConfig.SPRITE_DEFAULT_VELOCITY_X;
        public double VelocityY { get; private set; } = GameConfig.SPRITE_DEFAULT_VELOCITY_Y;
        public GameColor ForegroundColor { get; set; }
        public GameColor BackgroundColor { get; set; }
        public char DisplayCharacter { get; protected set; } = ' ';

        public TimeSpan TimeSinceMove { get; private set; }

        protected Point _position;
        public Point Position
        {
            get => _position with { }; protected set => _position = value;
        }
        public int ZIndex { get; }

        protected readonly List<ISprite> _sprites = new();
        protected readonly Queue<PositionTransform> _moveQueue = new Queue<PositionTransform>(2);
        protected PositionTransform _lastMove;


        public SpriteContainer(Point position, int zIndex, DirectionType direction, GameColor foregroundColor, GameColor backgroundColor, double velocityX, double velocityY)
        {
            Position = position;
            ZIndex = zIndex;
            Direction = direction;
            _lastMove = new PositionTransform(0, 0, direction);
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            VelocityX = velocityX;
            VelocityY = velocityY;
        }

        public SpriteContainer(Point position, int zIndex, DirectionType direction, GameColor foregroundColor, GameColor backgroundColor, double velocityX, double velocityY, char displayCharacter)
        {
            Position = position;
            ZIndex = zIndex;
            Direction = direction;
            _lastMove = new PositionTransform(0, 0, direction);
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
            VelocityX = velocityX;
            VelocityY = velocityY;
            DisplayCharacter = displayCharacter;
        }

        protected virtual void Build()
        {
            Add(CreateSprite());            
            SpriteContainerChanged?.Invoke(this);
        }

        protected virtual Sprite CreateSprite()
        {
            var sampleSprite = _sprites.FirstOrDefault();

            var timeSinceMove = sampleSprite is null
                ? new TimeSpan()
                : sampleSprite.TimeSinceMove;

            var sprite = new Sprite(Position, ZIndex, Direction, ForegroundColor, BackgroundColor, DisplayCharacter, VelocityX, VelocityY, timeSinceMove);

            return sprite;
        }
        
        public IEnumerable<ISprite> GetSprites() => _sprites;

        public virtual void InstantMove(PositionTransform transform)
        {
            var spritesToRemove = new List<ISprite>();
            var spritesToAdd = new List<ISprite>();

            foreach (var sprite in _sprites)
            {
                var newSprite = CreateSprite();
                spritesToRemove.Add(sprite);
                newSprite.InstantMove(transform);
                spritesToAdd.Add(newSprite);
            }
            RemoveRange(spritesToRemove);
            AddRange(spritesToAdd);
            Position = _position with 
            { 
                X = _position.X + transform.XDelta, 
                Y = _position.Y + transform.YDelta 
            };
            SpriteContainerChanged?.Invoke(this);
        }

        public void Move(long timeDelta)
        {
            if (!CanRender(timeDelta)) return;

            foreach (var sprite in _sprites)
            {
                sprite.Move(timeDelta);
            }
            Position = Direction switch
            {
                DirectionType.Down => Position with { Y = _position.Y + 1 },
                DirectionType.Up => Position with { Y = _position.Y - 1 },
                DirectionType.Left => Position with { Y = _position.X - 1 },
                DirectionType.Right => Position with { Y = _position.X + 1 },
                _ => throw new Exception("Invalid direction")
            };

            SpriteContainerChanged?.Invoke(this);
        }

        public virtual void Move(PositionTransform currentMove, long timeDelta)
        {
            _moveQueue.Enqueue(currentMove);

            if (!CanRender(timeDelta)) return;

            ExecuteMoves(timeDelta, _sprites);
            Position = _sprites.First().Position;
            SpriteContainerChanged?.Invoke(this);
        }

        protected virtual void ExecuteMoves(long timeDelta, IEnumerable<ISprite> sprites)
        {
            while(_moveQueue.Count > 0)
            {
                var move = _moveQueue.Dequeue();
                
                foreach (var sprite in sprites)
                {
                    SpriteDestroyed?.Invoke(sprite);
                    sprite.Move(move, timeDelta);
                    SpriteCreated?.Invoke(sprite);
                }
            }
        }

        protected virtual void ExecuteQueuedMoves(long timeDelta, Sprite sprite)
        {
            while (_moveQueue.Count > 0)
            {
                var move = _moveQueue.Dequeue();

                sprite.Move(move, timeDelta);
            }
        }

        public virtual bool CanRender(long timeDelta)
        {
            if (_sprites.Any() is false) return false;

            return _sprites
             .First()
             .CanRender(timeDelta);
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
            //sprite.SpriteCreated += SpriteCreated;            
            _sprites.Add(sprite);
            sprite.SpriteDestroyed += OnSpriteDestroyed;
            SpriteCreated?.Invoke(sprite);
        }

        private void OnSpriteDestroyed(ISprite sprite)
        {
            SpriteDestroyed?.Invoke(sprite);
        }

        protected void AddRange(IEnumerable<ISprite> sprites)
        {
            _sprites.AddRange(sprites);
            _sprites.ForEach(sprite => SpriteCreated?.Invoke(sprite));
        }

        protected void RemoveRange(IEnumerable<ISprite> sprites)
        {
            foreach (ISprite sprite in sprites)
            {
                _sprites.Remove(sprite);
                SpriteDestroyed?.Invoke(sprite);
            }            
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
