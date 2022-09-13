using Nibbles.GameObject.Dimensions;

namespace Nibbles.GameObject.Abstractions
{
    public interface ISpriteContainer
    {
        Position GetPosition();
        IEnumerable<ISprite> GetSprites();
    }
}
