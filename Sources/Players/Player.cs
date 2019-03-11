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

        // Print the boards of player
        public void PrintBoards()
        {
            Console.WriteLine(Name + "'s PrimaryBoard: ");
            PrimaryBoard.PrintBoard();

            Console.WriteLine(Name + "'s TrackingBoard: ");
            TrackingBoard.PrintBoard();
        }

        // Add ships randomly to the board
        public void AddShips()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach(Ship ship in Ships)
            {
                bool IsAdded = false;
                while(!IsAdded){
                    Point p0 = new Point(rand.Next(1,PrimaryBoard.Size + 1), rand.Next(1,PrimaryBoard.Size + 1));
                    bool horizontal = Convert.ToBoolean(rand.Next(2));
                    IsAdded = PrimaryBoard.PlaceShip(p0, horizontal, ship);
                }
            }
        }

        // Process shot taken by opponent
        public int ProcessShot(Point p)
        {
            int result = PrimaryBoard.ReceiveShot(p);
            if( result == 0)
            {
                Console.WriteLine("Shot miss.");
            }
            else if(result > 0)
            {
                var ship = Ships.First(x => x.Id == result);
                ship.Hits++;

                Console.WriteLine(Name + "'s " + ship.Name + " is Hit.");
                if(ship.IsSunk)
                {
                    Console.WriteLine(Name + "'s " + ship.Name + " is Sunk.");
                }
            }
            else
            {
                Console.WriteLine("Coordinate is Invalid.");
            }
            return result;
        }

        // Report result of shot on tracking board
        public void ReportShot(Point p, int result)
        {
            TrackingBoard.ReportShot(p, result);
        }

        // If the player is lost
        public bool IsLost()
        {
                return Ships.All(x => x.IsSunk);
        }
    }
}