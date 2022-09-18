using Nibbles.GameObject.Configuration;
using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.Abstractions
{
    public interface ISprite
    {
        GameColor ForegroundColor { get; }
        GameColor BackgroundColor { get; }
        char DisplayCharacter { get; }
        Point GetPosition();
        double GetVelocity();
        void Move(PositionTransform transform, long timeDelta);
        void Move(long timeDelta);
        bool CanRender(long timeDelta);
    }
}
