using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeGame
{
    public class Day7
    {
        public Day7()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day7\\Day7Input.txt");

            List<int>crabPositions = new List<int>();
            crabPositions = allData[0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();

            var result = crabPositions.GroupBy(n => n)
                    .Select(c => new { Position = c.Key, total = c.Count() }).OrderBy(x=>x.Position);

            List<Tuple<int, int>> crabPositionOccurrence = new List<Tuple<int, int>>(); 

            foreach (var item in result)
            {
                crabPositionOccurrence.Add(new Tuple<int, int>(item.Position, item.total));
            }

            Console.WriteLine("Day 7:");
            
            Part1(crabPositionOccurrence);


            Part2(crabPositions);

            Console.WriteLine();
        }


        private void Part1(List<Tuple<int, int>> crabPositionOccurrence)
        {
            /* Position, Fuel */
            List<Tuple<int, int>> allFuelConsumption = new List<Tuple<int, int>>();

            for (int i = 0; i < crabPositionOccurrence.Count(); i++)
            {
                int FuelConsum = 0;

                for (int j = 0; j < crabPositionOccurrence.Count(); j++)
                {
                    FuelConsum += Math.Abs((crabPositionOccurrence[i].Item1 - crabPositionOccurrence[j].Item1) * crabPositionOccurrence[j].Item2);
                }

                allFuelConsumption.Add(new Tuple<int, int>(crabPositionOccurrence[i].Item1, FuelConsum));
            }

            var result = allFuelConsumption.OrderBy(x => x.Item2).First();

            Console.WriteLine("Part 1 Solution: " + "To Position: " + result.Item1 + "   Requires: " + result.Item2 +" Fuel");
        }

        private void Part2(List<int> crabPositions)
        {
            /* Position, Fuel */
            List<Tuple<int, int>> allFuelConsumption = new List<Tuple<int, int>>();

            for (int i = 0; i < crabPositions.Max(); i++)
            {
                int FuelConsum = 0;

                for (int j = 0; j < crabPositions.Count(); j++)
                {
                    int steps = Math.Abs(crabPositions[j] - i);

                    FuelConsum += (((Int32)Math.Pow(steps, 2) + steps) / 2);
                }

                allFuelConsumption.Add(new Tuple<int, int>(i, FuelConsum));
            }

            var result = allFuelConsumption.OrderBy(x => x.Item2).First();

            Console.WriteLine("Part 2 Solution: " + "To Position: " + result.Item1 + "   Requires: " + result.Item2 + " Fuel");
        }
    }
}
