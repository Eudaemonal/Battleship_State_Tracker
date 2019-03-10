using BattleshipStateTracker.Sources.Boards;
using BattleshipStateTracker.Sources.Ships;
using BattleshipStateTracker.Utils;

using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipStateTracker.Sources.Players
{
    class Player
    {
        public string Name {get; set; }
        public Board PrimaryBoard {get; set; }
        public Board TrackingBoard {get; set; }
        public List<Ship> Ships {get; set; }

        public Player(string name){
            Name = name;
            PrimaryBoard = new Board();
            TrackingBoard = new Board();
            Ships  = new List<Ship>{
                new Carrier(1),
                new Battleship(2),
                new Cruiser(3),
                new Destroyer(4),
                new Destroyer(5),
                new Submarine(6),
                new Submarine(7)
            };
        }

        // print the boards of player
        public void PrintBoards()
        {
            Console.WriteLine(Name + " PrimaryBoard: ");
            PrimaryBoard.PrintBoard();

            Console.WriteLine(Name + " TrackingBoard: ");
            TrackingBoard.PrintBoard();
        }
        public void AddShips()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach(Ship ship in Ships)
            {
                bool IsAdded = false;
                while(!IsAdded){
                    Point p0 = new Point(rand.Next(1,11), rand.Next(1,11));
                    bool horizontal = Convert.ToBoolean(rand.Next(2));
                    IsAdded = PrimaryBoard.PlaceShip(p0, horizontal, ship);
                }
            }
        }

        public int ProcessShot(Point p)
        {
            int Result = PrimaryBoard.ReceiveShot(p);
            if( Result == 0)
            {
                Console.WriteLine("Shot miss");
            }
            else 
            {
                var ship = Ships.First(x => x.Id == Result);
                ship.Hits++;

                Console.WriteLine(ship.Name + " is Hit");
                if(ship.IsSunk)
                {
                    Console.WriteLine(ship.Name + " is Sunk");
                }
            }
            return Result;
        }

        public void ReportShot(Point p, int Result)
        {
            TrackingBoard.ReportShot(p, Result);
        }

        public bool IsLost()
        {
                return Ships.All(x => x.IsSunk);
        }
        

    }
}