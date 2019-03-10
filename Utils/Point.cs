namespace BattleshipStateTracker.Utils
{
    public class Point{
        // Vertical coordinate, Row number
        public int Row {get; set; }
        
        // Horizontal coordinate, Col number
        public int Col {get; set; }

        // Constructor
        public Point(int row, int col)
        {
            Col = col;
            Row = row;
        }

        // Copy Constructor
        public Point(Point p)
        {
            Row = p.Row;
            Col = p.Col;
        }

        public void Set(int row, int col)
        {
            Col = col;
            Row = row;
        }
    }
}