using Nibbles.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibbles.Engine.Abstractions
{
    public interface IInputReader
    {
        InputType Get();
    }
}
