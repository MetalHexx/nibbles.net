using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Abstractions
{
    public interface ISprite
    {
        public ConsoleColor ForegroundColor { get; }
        public ConsoleColor BackgroundColor { get; }
        public char DisplayCharacter { get; }
        public Position GetPosition();
        public void Move(PositionTransform transform, long timeDelta);
        bool ShouldMove(PositionTransform transform, long timeDelta);
    }
}
