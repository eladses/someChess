using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenderChess.CODE
{
    public class Troop    {
        public bool Alive { get; set; }
        public UnitTypes Type { get; set; }
        public GenderTypes Gender { get; set; }
        public Coords Location { get; set; }
        public bool color { get; set; }

        public Troop(bool Alive, UnitTypes Type, GenderTypes Gender, Coords Location)
        {
            this.Alive = Alive;
            this.Type = Type;
            this.Gender = Gender;
            this.Location = Location;
        }
    }
}
