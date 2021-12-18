using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeGame
{
    public class Day13
    {
        public Day13()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day13\\Day13Input.txt");

            Console.WriteLine("Day 13:");

            List<Tuple<string, int>> foldings = new List<Tuple<string, int>>();
            List<Tuple<int, int>> positions = new List<Tuple<int, int>>();

            /* separate the foldings and the coordinates */
            foreach (string line in allData)
            {
                if (line.Contains("fold along"))
                {
                    List<string> tmpLine = line.Remove(0, 11).Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foldings.Add(new Tuple<string, int>(tmpLine[0], Int32.Parse(tmpLine[1])));
                }
                else
                {
                    List<int> result = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();

                    if (result.Count() != 0)
                    {
                        positions.Add(new Tuple<int, int>(result[0], result[1]));
                    }                    
                }
            }

            /* Set table size */
            int maxColsize = positions.OrderByDescending(x => x.Item1).ToList().First().Item1 + 1;
            int maxRowsize = positions.OrderByDescending(x => x.Item2).ToList().First().Item2 + 1;

            Paper TransparentTablePart1 = new Paper(maxColsize, maxRowsize);

            /* Set the initial dots on the paper for part 1 and part 2 */
            foreach (var position in positions)
            {
                TransparentTablePart1.SetStartingPosition(position.Item1, position.Item2);
            }

            Paper TransparentTablePart2 = new Paper(maxColsize, maxRowsize);

            foreach (var position in positions)
            {
                TransparentTablePart2.SetStartingPosition(position.Item1, position.Item2);
            }

            Part1(TransparentTablePart1, foldings);

            Part2(TransparentTablePart2, foldings);

            Console.WriteLine();
        }

        private void Part1(Paper transparentTable, List<Tuple<string, int>> foldings)
        {
            transparentTable.FoldPaper(foldings[0].Item1, foldings[0].Item2);

            Console.WriteLine("Part 1 Solution: " + transparentTable.GetDotsOnPaper());
        }

        private void Part2(Paper transparentTable, List<Tuple<string, int>> foldings)
        {
            foreach (var position in foldings)
            {
                transparentTable.FoldPaper(position.Item1, position.Item2);
            }

            Console.WriteLine("Part 2 Solution: ");
            transparentTable.PrintTable();

        }
    }
}
