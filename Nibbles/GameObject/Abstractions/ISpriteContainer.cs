using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.Abstractions
{
    public interface ISpriteContainer: ISprite
    {
        IEnumerable<ISprite> GetSprites();
    }
}
