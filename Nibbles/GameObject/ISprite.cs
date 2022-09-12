namespace Nibbles.GameObject
{
    public interface ISprite
    {
        public ConsoleColor ForegroundColor { get; }
        public ConsoleColor BackgroundColor { get; }
        public char DisplayCharacter { get; }
        public Position GetPosition();
    }
}
