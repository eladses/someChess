using GenderChess.CODE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessStuff
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }



    public class ChessRules
    {
        GameDataManager gameData;
        public static List<GenderTypes> gendersList = new List<GenderTypes>() { GenderTypes.Default, GenderTypes.Default, GenderTypes.Default };
        //set the board and call to the first turn
        public void StartGame()
        {
            for(int i = 0; i<12; i++)
            {
                gendersList.Add((GenderTypes)new Random().Next(5));
            }
            startBoard.createWList();
            startBoard.createBList();
            gameData = new GameDataManager(8, 8, true, startBoard.W, startBoard.B);
        }

        public class Movements
        {
            public Troop Troop { get; set; }
            public List<Coords> PossibleMovements { get; set; }

            public Movements(Troop Troop, List<Coords> PossibleMovements)
            {
                this.Troop = Troop;
                this.PossibleMovements = PossibleMovements;
            }
        }

        private List<Movements> LastMoveables;

        //return list of moveable units
        public List<Troop> StartTurn()
        {
            gameData.NextTurn();
            List<Troop> Units = gameData.GetCurrentLiving();
            LastMoveables = new List<Movements>();
            foreach(Troop t in Units)
            {
                Movements move = new Movements(t, unitToVectors(t));
                if (move.PossibleMovements.Count != 0)
                {
                    LastMoveables.Add(move);
                }
            }
            List<Troop> output = new List<Troop>();
            foreach(Movements m in LastMoveables)
            {
                output.Add(m.Troop);
            }
            return output;
        }

        public List<Coords> unitToVectors(Troop troop)
        {
            List<Coords> output = new List<Coords>();
            bool queenFlag = false;
            if (troop.Gender == GenderTypes.Checkers)
            {
                lineVectorGenerator(-1, -1, troop, output, troop.Location);
                lineVectorGenerator(+1, -1, troop, output, troop.Location);
                lineVectorGenerator(-1, +1, troop, output, troop.Location);
                lineVectorGenerator(+1, +1, troop, output, troop.Location);
                return output;
            }
            switch (troop.Type) {
                case UnitTypes.Pawn:
                    if (!troop.color)
                    {
                        if (troop.Location.Row == 6)
                        {
                            output.Add(new Coords(troop.Location.Column, 4));
                        }
                        output.Add(new Coords(troop.Location.Column, troop.Location.Row - 1));
                        Coords tempC = new Coords(troop.Location.Column - 1, troop.Location.Row - 1);
                        Troop temp = gameData.GetTroopFromMap(tempC);
                        if (!temp.Equals(null))
                        {
                            if (temp.color)
                            {
                                output.Add(temp.Location);
                            }
                        }
                        tempC.Column += 2;
                        temp = gameData.GetTroopFromMap(tempC);
                        if (!temp.Equals(null))
                        {
                            if (temp.color)
                            {
                                output.Add(temp.Location);
                            }
                        }

                    }
                    else
                    {
                        if (troop.Location.Row == 1)
                        {
                            output.Add(new Coords(troop.Location.Column, 3));
                        }
                        output.Add(new Coords(troop.Location.Column, troop.Location.Row + 1));
                        Coords tempC = new Coords(troop.Location.Column - 1, troop.Location.Row + 1);
                        Troop temp = gameData.GetTroopFromMap(tempC);
                        if (temp.Equals(null))
                        {
                            if (!temp.color)
                            {
                                output.Add(temp.Location);
                            }
                        }
                        tempC.Column += 2;
                        temp = gameData.GetTroopFromMap(tempC);
                        if (temp.Equals(null))
                        {
                            if (!temp.color)
                            {
                                output.Add(temp.Location);
                            }
                        }
                    }
                    outputValidator(output, troop);
                    return output;
                case UnitTypes.King:
                    output.Add(new Coords(troop.Location.Column - 1, troop.Location.Row - 1)); output.Add(new Coords(troop.Location.Column, troop.Location.Row - 1)); output.Add(new Coords(troop.Location.Column + 1, troop.Location.Row - 1));
                    output.Add(new Coords(troop.Location.Column - 1, troop.Location.Row));                                                                            output.Add(new Coords(troop.Location.Column + 1, troop.Location.Row));
                    output.Add(new Coords(troop.Location.Column - 1, troop.Location.Row + 1)); output.Add(new Coords(troop.Location.Column, troop.Location.Row + 1)); output.Add(new Coords(troop.Location.Column + 1, troop.Location.Row + 1));
                    outputValidator(output, troop);
                    return output;
                case UnitTypes.Knight:
                    output.Add(new Coords(troop.Location.Column - 2, troop.Location.Row - 1)); output.Add(new Coords(troop.Location.Column - 2, troop.Location.Row + 1)); output.Add(new Coords(troop.Location.Column + 2, troop.Location.Row - 1));
                    output.Add(new Coords(troop.Location.Column + 2, troop.Location.Row + 1)); output.Add(new Coords(troop.Location.Column - 1, troop.Location.Row - 2));
                    output.Add(new Coords(troop.Location.Column - 1, troop.Location.Row + 2)); output.Add(new Coords(troop.Location.Column + 1, troop.Location.Row - 2)); output.Add(new Coords(troop.Location.Column + 1, troop.Location.Row + 2));
                    outputValidator(output, troop);
                    return output;
                case UnitTypes.Queen:
                    queenFlag = true;
                    goto case UnitTypes.Rook;
                case UnitTypes.Rook:
                    lineVectorGenerator(-1, 0, troop, output, troop.Location);
                    lineVectorGenerator(+1, 0, troop, output, troop.Location);
                    lineVectorGenerator(0, -1, troop, output, troop.Location);
                    lineVectorGenerator(0, +1, troop, output, troop.Location);
                    if (!queenFlag)
                    {
                        return output;
                    }
                    goto case UnitTypes.Bishop;
                case UnitTypes.Bishop:
                    lineVectorGenerator(-1, -1, troop, output, troop.Location);
                    lineVectorGenerator(+1, -1, troop, output, troop.Location);
                    lineVectorGenerator(-1, +1, troop, output, troop.Location);
                    lineVectorGenerator(+1, +1, troop, output, troop.Location);
                    return output;
            }
            return null; //if this happens, con-fucking-ratz!
        }

        private void lineVectorGenerator(int xdif, int ydif, Troop troop, List<Coords> output, Coords last)
        {
            Coords next = new Coords(last.Column + xdif, last.Row + ydif);
            if(!(-1 > next.Column && next.Column > 8 || -1 > next.Row && next.Row > 8 || troop.color == gameData.GetTroopFromMap(next).color))
            {
                if(troop.Gender == GenderTypes.Checkers)
                {
                    if (troop.color != gameData.GetTroopFromMap(next).color)
                    {
                        next.Column += xdif;
                        next.Row += ydif;
                        if (!(-1 > next.Column && next.Column > 8 || -1 > next.Row && next.Row > 8 || !gameData.GetTroopFromMap(next).Equals(null)))
                        {
                            output.Add(next);
                        }
                    }
                }
                else if(troop.Gender == GenderTypes.Pacifist)
                {
                    if (!gameData.GetTroopFromMap(next).Equals(null))
                    {
                        return;
                    }
                }
                output.Add(next);
                lineVectorGenerator(xdif, ydif, troop, output, next);
            }
        }

        private void outputValidator(List<Coords> output, Troop troop)
        {
            foreach (Coords coords in output)
            {
                if (-1 > coords.Column && coords.Column > 8 || -1 > coords.Row && coords.Row > 8 || troop.color == gameData.GetTroopFromMap(coords).color || troop.color != gameData.GetTroopFromMap(coords).color && troop.Gender == GenderTypes.Pacifist)
                {
                    output.Remove(coords);
                }
            }
        }

        public Movements SetChosenPeice(int x, int y)
        {
            Coords coords = new Coords(x, y);
            Troop troop = gameData.GetTroopFromMap(coords);
            foreach(Movements move in LastMoveables)
            {
                if (troop.Equals(move.Troop))
                {
                    return move;
                }
            }
            return null;
        }

        public void MoveChosenPiece(int TroopX, int TroopY, int NewX, int NewY)
        {
            Troop troop = gameData.GetTroopFromMap(new Coords(TroopX, TroopY));
            if (troop.Gender == GenderTypes.Checkers)
            {
                int xdif;
                int ydif;
                if (NewX > TroopX)
                {
                    xdif = 1;
                }
                else
                {
                    xdif = -1;
                }
                if (NewY > TroopY)
                {
                    ydif = 1;
                }
                else
                {
                    ydif = -1;
                }
                gameData.SetTroopToMap(troop, new Coords(NewX - xdif, NewY - ydif));
            }
            gameData.SetTroopToMap(troop, new Coords(NewX, NewY));
        }
    } 
}
