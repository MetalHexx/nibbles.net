using Nibbles.GameObject.Abstractions;

namespace Nibbles.Engine.Abstractions
{
    public interface ISpriteRenderer
    {
        void Remove(ISprite sprite);
        void Add(ISprite sprite);
        void AddRange(IEnumerable<ISprite> sprites);
        void Render();
    }
}