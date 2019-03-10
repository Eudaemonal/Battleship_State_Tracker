using BattleshipStateTracker.Sources.Ships;
using BattleshipStateTracker.Utils;

using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipStateTracker.Sources.Boards
{
    public class Board{
        public List<Square> Grid{get; set; }

        // Create an empty board
        public Board()
        {
            Grid = new List<Square>();
            for(int i = 1; i <= 10; i++)
            {
                for(int j = 1; j <= 10; j++)
                {
                    Grid.Add(new Square(i, j));
                }
            }
        }

        // Get the square at point p
        public Square At(Point p)
        {
            if(!IsValidCoordinate(p)) return null;
            return Grid.Where(x => x.Coordinate.Row == p.Row && x.Coordinate.Col == p.Col).First();
        }

        // Select the squares within p0 and p1
        // Note: both Row and Col index of p0 should be less than or equal to that of p1
        public List<Square> Select(Point p0, Point p1)
        {
            return Grid.Where(x => x.Coordinate.Row >= p0.Row 
                                && x.Coordinate.Col >= p0.Col
                                && x.Coordinate.Row <= p1.Row 
                                && x.Coordinate.Col <= p1.Col).ToList();
        }

        // Print the board
        public void PrintBoard()
        {
            for(int i = 1; i <= 10; i++)
            {
                for(int j = 1; j <= 10; j++)
                {
                    Console.Write((int)this.At(new Point(i, j)).Type + " ");
                }
                Console.Write(Environment.NewLine);
            }
        }

        // Check if the coordinate within the board
        public bool IsValidCoordinate(Point p)
        {
            if(1 <= p.Row && p.Row <= 10 && 1 <= p.Col && p.Col <= 10 )
            {
                return true;
            }
            return false;
        }


        // Place ship on the primary board
        public bool PlaceShip(Point p0, bool horizontal, Ship ship)
        {
            // Calculate the end point p1 based on start point p0
            Point p1;
            if(horizontal)
            {
                p1 = new Point(p0.Row, p0.Col + ship.Size - 1);
            }
            else
            {
                p1 = new Point(p0.Row + ship.Size - 1, p0.Col);
            }

            // Check if ship in the board
            if(!IsValidCoordinate(p0) || !IsValidCoordinate(p1))
            {
                return false;
            }

            // Set the board
            List<Square> occupiedSquares = Select(p0, p1);
            if(occupiedSquares.Any(x=>x.IsShip()))
            {
                return false;
            }
            
            // Change Square types occupied by the ship
            foreach(var square in occupiedSquares)
            {
                square.Type = SquareType.Ship;
                square.ShipId = ship.Id;
            }
            return true;
        }

        // Process the status of primary board after the opponent take a shot at p
        // return ShipId if hit, 0 if miss
        public int ReceiveShot(Point p)
        {
            Square square = this.At(p);
            if(square.IsEmpty())
            {
                square.Type = SquareType.Miss;
                return 0;
            }
            square.Type = SquareType.Hit;

            return square.ShipId;
        }

        // Process the status of tracking board after take a shot
        public void ReportShot(Point p, int Result)
        {
            var square = this.At(p);
            if(Result == 0 )
            {
                square.Type = SquareType.Miss;
            }
            else
            {
                square.Type = SquareType.Hit;
            }
        }
    }
}