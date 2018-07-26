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
        static ChessRules chessRules = new ChessRules();
        static void Main(string[] args)
        {           
            chessRules.StartGame();
            chessRules.StartTurn();
            takeATurn();
            
        }

        static void takeATurn()
        {
            Console.WriteLine("pick a Piece");
            sendChosenPiece();

            Console.WriteLine("legal moves:");
            Console.WriteLine();//List ligal moves

            Console.WriteLine("make a move");
            sendChosenMove();

        }
        static void sendChosenPiece()
        {
            Console.Write("x: ");
            int x = int.Parse(Console.ReadLine());
            Console.Write("y: ");
            int y = int.Parse(Console.ReadLine());

            chessRules.SetChosenPeice(x, y);   
        }

        static void sendChosenMove()
        {
            Console.Write("x: ");
            int x = int.Parse(Console.ReadLine());
            Console.Write("y: ");
            int y = int.Parse(Console.ReadLine());

            //chessRules.
        }
    }







    public class ChessRules
    {
        GameDataManager gameData;
        private int currentPlayer;

        //set the board and call to the first turn
        public void StartGame()
        {
            gameData = new GameDataManager(8, 8, false, new List<Troop>() { new Troop(true, UnitTypes.Pawn, null, new Coords(5, 6), 0) }, new List<Troop>());
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
                Movements move = new Movements(t, new List<Coords> { new Coords(0, 0), new Coords(1, 1) });
                if (!move.PossibleMovements.Equals(null))
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

        //get poss in the board and call "WhereCanMove" with the unit

        //get the unit and return bool matrix of the posses 
        /*        private List<Coords> WhereCanMove(Troop troop)
                {
                    var table = new int[8, 8];
                    for (int y = 0; y < table.Length; y++)
                    {
                        for (int x = 0; x < table.Length; x++)
                        {
                            table[x, y] = CanIMove(troop, new Coords();
                        }
                    }
                    return table;
                } */

        //get poss and unit and check if this unit can move there
        /*        private bool CanIMove(Troop troop, Coords coords)
                {
                    var isValid = true;

                    switch (troop.Type)
                    {
                        case UnitTypes.King:

                            break;
                        case UnitTypes.Queen:
                            break;
                        case UnitTypes.Bishop:
                            break;
                        case UnitTypes.Pawn:
                            break;
                        case UnitTypes.Knight:
                            break;
                        case UnitTypes.Rook:
                            break;
                    }

                    var unitAtTile = gameData.UnitIn(x, y);
                    if (unitAtTile.player = currentPLayer)
                    {
                        isValid = false;
                    }
                    else
                    {
                        if (unit.pacifist)
                        {
                            isValid = false;
                        }
                    }
                    return isValid;
                }

                public void MoveUnit(int x, int y)
                {
                    Coords coords = new Coords(x, y);
                    gameData.SetTroopToMap(gameData.GetTroopFromMap(coords), coords);
                    //turn end
                } */

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
            gameData.SetTroopToMap(gameData.GetTroopFromMap(new Coords(TroopX, TroopY)), new Coords(NewX, NewY));
        }
    } 



    //
    // var units = GetUnits();
    // ...
    // MoveUnit(currentUnit,x,y);
    // var newUnits = GetUnits();
    // //check who moved, and who no longer exists and animate accordingly
    // ...
    // StartTurn(otherPlayer);
    //
    //






}
