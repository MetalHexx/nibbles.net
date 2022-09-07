using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nibbles
{
    public  class GameBoard
    {
        public int MinX { get; private set; } = -1;
        public int MaxX { get; private set; } = Console.BufferWidth;
        public int MinY { get; private set; } = -1;
        public int MaxY { get; private set; } = Console.BufferHeight;
    }
}
