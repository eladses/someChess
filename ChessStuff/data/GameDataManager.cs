using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenderChess.CODE
{
    class GameDataManager    {
        private Troop[,] Map;
        private bool Turn; // true = white, false = black. not racist just facts :P
        private List<Troop> WhiteTroops;
        private List<Troop> BlackTroops;

        public GameDataManager(int MapLength, int MapHeight, bool StartingTurn, List<Troop> WhiteTroops, List<Troop> BlackTroops)
        {
            Turn = StartingTurn;
            Map = new Troop[MapLength,MapHeight];
            this.WhiteTroops = WhiteTroops;
            this.BlackTroops = BlackTroops;
            foreach(Troop Troop in WhiteTroops)
            {
                Map[Troop.Location.Row, Troop.Location.Column] = Troop;
                Troop.color = true;
            }
            foreach (Troop Troop in BlackTroops)
            {
                Map[Troop.Location.Row, Troop.Location.Column] = Troop;
                Troop.color = false;
            }
        }

        public List<Troop> GetWhiteTroops()
        {
            List<Troop> Result = new List<Troop>();
            foreach (Troop Troop in WhiteTroops)
            {
                if (Troop.Alive)
                {
                    Result.Add(Troop);
                }
            }
            return Result;
        }

        public List<Troop> GetDeadWhiteTroops()
        {
            List<Troop> Result = new List<Troop>();
            foreach (Troop Troop in WhiteTroops)
            {
                if (!Troop.Alive)
                {
                    Result.Add(Troop);
                }
            }
            return Result;
        }

        public List<Troop> GetBlackTroops()
        {
            List<Troop> Result = new List<Troop>();
            foreach (Troop Troop in BlackTroops)
            {
                if (Troop.Alive)
                {
                    Result.Add(Troop);
                }
            }
            return Result;
        }

        public List<Troop> GetDeadBlackTroops()
        {
            List<Troop> Result = new List<Troop>();
            foreach (Troop Troop in BlackTroops)
            {
                if (!Troop.Alive)
                {
                    Result.Add(Troop);
                }
            }
            return Result;
        }

        public List<Troop> GetCurrentLiving()
        {
            if (Turn)
            {
                return GetWhiteTroops();
            }
            else
            {
                return GetBlackTroops();
            }
        }

        public List<Troop> GetCurrentDead()
        {
            if (Turn)
            {
                return GetDeadWhiteTroops();
            }
            else
            {
                return GetDeadBlackTroops();
            }
        }

        public Troop GetTroopFromMap(Coords Coords)
        {
            return Map[Coords.Column, Coords.Row];
        }

        public void SetTroopToMap(Troop Troop, Coords NewCoords)
        {
            Map[Troop.Location.Row, Troop.Location.Column] = null;
            Troop temp = Map[NewCoords.Column, NewCoords.Row];
            if (!temp.Equals(null))
            {
                temp.Alive = false;
            }
            temp = Troop;
        }

        public void NextTurn()
        {
            Turn = !Turn;
        }
    }
}
