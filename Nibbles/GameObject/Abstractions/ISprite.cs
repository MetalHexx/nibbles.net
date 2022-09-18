using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.Abstractions
{
    public interface ISprite
    {
        ConsoleColor ForegroundColor { get; }
        ConsoleColor BackgroundColor { get; }
        char DisplayCharacter { get; }
        Point GetPosition();
        double GetVelocity();
        void Move(PositionTransform transform, long timeDelta);
        void Move(long timeDelta);
        bool ShouldMove(long timeDelta);
    }
}
