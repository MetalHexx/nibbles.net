using Nibbles.GameObject.Dimensions;
using System.Drawing;

namespace Nibbles.GameObject.Abstractions
{
    public interface ISpriteContainer: ISprite
    {
        Action<ISpriteContainer>? SpriteContainerChanged { get; set; }
        Guid Id { get; }
        IEnumerable<ISprite> GetSprites();
    }
}
