using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.Abstractions
{
    public interface ISpriteContainer
    {
        Point GetPosition();
        IEnumerable<ISprite> GetSprites();
    }
}
