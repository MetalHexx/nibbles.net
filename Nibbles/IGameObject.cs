using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibbles
{
    public interface IGameObject
    {
        public GameObjectPosition Position { get; }
    }
}
