namespace Nibbles.GameObject
{
    public interface ISpriteContainer
    {
        public Position GetPosition();
        IEnumerable<ISprite> GetSprites();
    }
}
