using BattleshipStateTracker.Sources.Players;
using BattleshipStateTracker.Utils;
using System;

namespace BattleshipStateTracker.Sources
{
    class Game
    {
        public Player Player1{get; set; }
        public Player Player2{get; set; }

        public Game(string name1, string name2)
        {
            Player1 = new Player(name1);
            Player2 = new Player(name2);
        }

        public void Run()
        {
            Point target = new Point(0,0);
            string[] tokens;
            int result;

            while(!Player1.IsLost() && !Player2.IsLost())
            {
                // Player 1 Attack first
                tokens = Console.ReadLine().Split();
                target.Set(int.Parse(tokens[0]), int.Parse(tokens[1]));

                result = Player2.ProcessShot(target);
                Player1.ReportShot(target, result);

                if(Player2.IsLost())
                {
                    Console.WriteLine(Player1.Name + " has win!");
                    break;
                }

                // Player 2 Attack next
                tokens = Console.ReadLine().Split();
                target.Set(int.Parse(tokens[0]), int.Parse(tokens[1]));

                result = Player1.ProcessShot(target);
                Player2.ReportShot(target, result);
                
                if(Player1.IsLost()){
                    Console.WriteLine(Player2.Name + " has win!");
                    break;
                }
            }
        }

    }
}