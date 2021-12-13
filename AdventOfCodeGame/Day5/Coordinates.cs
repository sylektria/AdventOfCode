namespace Day5
{
    public class Coordinates
    {
        /* Column and Row */
        public int X1 { get; set; }
        public int Y1 { get; set; }

        /* Column and Row */
        public int X2 { get; set; }        
        public int Y2 { get; set; }

        public Coordinates(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;

            X2 = x2;            
            Y2 = y2;
        }

    }
}
