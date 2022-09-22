using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.Abstractions
{
    public interface ISprite
    {
        Action<ISprite>? SpriteDestroyed { get; set; }
        Action<ISprite>? SpriteCreated { get; set; }
        Point Position { get; }
        public int ZIndex { get; }
        DirectionType Direction { get; }
        GameColor ForegroundColor { get; }
        GameColor BackgroundColor { get; }
        char DisplayCharacter { get; }
        void Move(PositionTransform transform, long timeDelta);
        void Move(long timeDelta);
        bool CanRender(long timeDelta);
        double GetVelocity();
    }
}
