using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenderChess.CODE
{
    public struct Coords    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Coords(int x, int y)
        {
            Row = y;
            Column = x;
        }
    }
}
