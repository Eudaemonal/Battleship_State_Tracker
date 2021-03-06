﻿using BattleshipStateTracker.Sources.Players;
using BattleshipStateTracker.Utils;


using System;


namespace BattleshipStateTracker
{
    class Program
    {

        static void Test()
        {
            // Create a board
            Player p1 = new Player("Player 1");
            Player p2 = new Player("Player 2");

            // Add a battleship to the board
            p1.AddShips();
            p2.AddShips();

            p1.PrintBoards();
            p2.PrintBoards();

            // Take an “attack” at a given position, and report back  
            // whether the attack resulted in a hit or a miss
            Point target = new Point(0, 0);
            int result = -1;
            while(result == -1){
                Console.WriteLine("Enter a point to attack: ");
                string[] tokens = Console.ReadLine().Split();
                target.Set(int.Parse(tokens[0]), int.Parse(tokens[1]));
                result = p2.ProcessShot(target);
            }
            p1.ReportShot(target, result);

            // Return whether the player has lost the game yet
            if (p2.IsLost())
            {
                Console.WriteLine(p2.Name + " is lost");
            }
        }

        static void Main(string[] args)
        {
            Test();
        }
    }
}
