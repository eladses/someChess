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
        public List<GenderTypes> Gender { get; set; }
        public Coords Location { get; set; }
        public int ID { get; }

        public Troop(bool Alive, UnitTypes Type, List<GenderTypes> Gender, Coords Location, int ID)
        {
            this.Alive = Alive;
            this.Type = Type;
            this.Gender = Gender;
            this.Location = Location;
            this.ID = ID;
        }
    }
}
