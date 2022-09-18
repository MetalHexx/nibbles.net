using Nibbles.GameObject.Abstractions;
using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Nibbles.GameObject.Projectiles
{
    public record Venom: Sprite
    {
        public Action<Venom>? VenomDestroyed;

        public int DistanceTraveled { get; private set; }
        public int MaxDistance { get; private set; }
        public Venom(Point position, DirectionType direction)
        : base(position, direction, SpriteConfig.VENOM_FOREGROUND_COLOR, SpriteConfig.VENOM_BACKGROUND_COLOR, ' ', SpriteConfig.VENOM_VELOCITY_X, SpriteConfig.VENOM_VELOCITY_Y) 
        {
            SetMaxDistance();
            AdjustPosition();
        }
        //Adjusts the position of the venom one step ahead of the object that generated it
        //other wise it makes the parent object flicker
        public void AdjustPosition()
        {
             Position = Direction switch
            {
                DirectionType.Up => Position with { Y = Position.Y - 2 },
                DirectionType.Down => Position with { Y = Position.Y + 2 },
                DirectionType.Left => Position with { X = Position.X - 2 },
                DirectionType.Right => Position with { X = Position.X + 2 },
                _ => Position
            };
        }

        public override void Move(long timeDelta)
        {
            if (!CanRender(timeDelta)) return;

            if (DistanceTraveled > MaxDistance)
            {
                VenomDestroyed?.Invoke(this with { });
                return;
            }
            DistanceTraveled++;

            base.Move(timeDelta);
        }

        private void SetMaxDistance()
        {
            MaxDistance = Direction switch
            {
                DirectionType.Up => SpriteConfig.VENOM_DISTANCE_Y,
                DirectionType.Down => SpriteConfig.VENOM_DISTANCE_Y,
                DirectionType.Left => SpriteConfig.VENOM_DISTANCE_X,
                DirectionType.Right => SpriteConfig.VENOM_DISTANCE_X,
            };
        }
    }
}
