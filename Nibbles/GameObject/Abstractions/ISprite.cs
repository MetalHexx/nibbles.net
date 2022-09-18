using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Abstractions
{
    public interface ISprite
    {
        ConsoleColor ForegroundColor { get; }
        ConsoleColor BackgroundColor { get; }
        char DisplayCharacter { get; }
        Position GetPosition();
        double GetVelocity();
        void Move(PositionTransform transform, long timeDelta);
        void Move(long timeDelta);
        bool ShouldMove(long timeDelta);
    }
}
