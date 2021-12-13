using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeGame
{
    public class LineOfData
    {
        public List<string> FirstPart { get; set; }
        public List<string> SecondPart { get; set; }

        public LineOfData(List<string> firstPart, List<string> secondPart)
        {
            SecondPart = secondPart;
            FirstPart = firstPart;
        }

    }
}
