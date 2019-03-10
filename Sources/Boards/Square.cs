using BattleshipStateTracker.Utils;

namespace BattleshipStateTracker.Sources.Boards
{

    public class Square
    {
        public SquareType Type{get; set; }
        public Point Coordinate{get; set; }
        public int ShipId {get; set; }

        // Constructor
        public Square(int x, int y)
        {
            Coordinate = new Point(x, y);
            Type = SquareType.Empty;
            ShipId = 0;
        }

        public bool IsShip()
        {
            return Type == SquareType.Ship;
        }

        public bool IsEmpty()
        {
            return Type == SquareType.Empty;
        }


    }
}