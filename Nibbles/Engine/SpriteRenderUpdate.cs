using Nibbles.GameObject.Abstractions;

namespace Nibbles.Engine
{
    public class SpriteRenderUpdate
    {
        public readonly List<ISprite> SpritesToAdd = new List<ISprite>();
        public readonly List<ISprite> SpritesToRemove = new List<ISprite>();

        public void Add(ISprite sprite)
        {
            SpritesToAdd.Add(sprite);
        }

        public void Add(IEnumerable<ISprite> sprites)
        {
            SpritesToAdd.AddRange(sprites);
        }
        public void Remove(ISprite sprite)
        {
            SpritesToRemove.Add(sprite);
        }
    }
}
