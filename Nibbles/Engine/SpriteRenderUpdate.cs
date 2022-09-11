using Nibbles.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Remove(ISprite sprite)
        {
            SpritesToRemove.Add(sprite);
        }
    }
}
