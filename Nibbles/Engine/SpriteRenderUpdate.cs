using Nibbles.GameObject.Abstractions;

namespace Nibbles.Engine
{
    public class SpriteRenderUpdate
    {
        private readonly List<ISprite> spritesToAdd = new();
        public readonly List<ISprite> SpritesToRemove = new();

        public List<ISprite> SpritesToAdd => spritesToAdd;

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
