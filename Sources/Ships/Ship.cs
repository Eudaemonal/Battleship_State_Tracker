namespace BattleshipStateTracker.Sources.Ships
{
    // Abstract Class of Ship
    public abstract class Ship
    {
        public string Name { get; set; }
        public int Id {get; set; }
        public int Size { get; set; }
        public int Hits { get; set; }
        public bool IsSunk
        {
            get
            {
                return Hits >= Size;
            }
        }
    }

    public class Carrier : Ship
    {
        public Carrier(int id)
        {
            Name = "Aircraft Carrier";
            Id = id;
            Size = 5;
        }
    }

    public class Battleship : Ship
    {
        public Battleship(int id)
        {
            Name = "Battleship";
            Id = id;
            Size = 4;
        }
    }

    public class Cruiser : Ship
    {
        public Cruiser(int id)
        {
            Name = "Cruiser";
            Id = id;
            Size = 3;
        }
    }

    public class Destroyer : Ship
    {
        public Destroyer(int id)
        {
            Name = "Destroyer";
            Id = id;
            Size = 2;
        }
    }

    public class Submarine : Ship
    {
        public Submarine(int id)
        {
            Name = "Submarine";
            Id = id;
            Size = 1;
        }
    }
}