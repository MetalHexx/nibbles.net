using Nibbles.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibbles
{
    internal class BulletHandler : IGameObjectEventHandler
    {
        public ISprite Handle(ISprite gameObject)
        {
            throw new NotImplementedException();
        }
    }

    internal class SnakeHandler: IGameObjectEventHandler
    {
        public ISprite Handle(ISprite gameObject)
        {
            throw new NotImplementedException();
        }
    }

    internal interface IGameObjectEventHandler
    {
        public ISprite Handle(ISprite gameObject);
    }
}
