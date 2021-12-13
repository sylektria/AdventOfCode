using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeGame
{
    public class Day3
    {
        public Day3()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day3\\Day3Input.txt");

            Console.WriteLine("Day 3:");

            Part1(allData);

            Part2(allData);

            Console.WriteLine();
        }

        private void Part1(List<string> allData)
        {
            string gammaRate = string.Empty;
            string epsilonRate = string.Empty;

            for (int i = 0; i < allData[0].Length; i++)
            {
                var result = allData.Select(fs => fs.Substring(i, 1)).GroupBy(item => item).OrderByDescending(g => g.Count()).Select(g => g.Key);

                gammaRate += result.First();
                epsilonRate += result.Last();
            }
            
            Console.WriteLine("Part 1 Solution: " + Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2));

        }

        private void Part2(List<string> allData)
        {
            string mostCommon = string.Empty;
            string leastCommon = string.Empty;

            var result = allData.Select(fs => fs.Substring(0, 1)).GroupBy(item => item).OrderByDescending(g => g.Count()).Select(g => g.Key);

            mostCommon += result.First();
            leastCommon += result.Last();

            var mostCommonItems = allData.Select(x=>x).Where(s => s.Substring(0, 1).Equals(mostCommon)).ToList();
            var leastCommonItems = allData.Select(x => x).Where(s => s.Substring(0, 1).Equals(leastCommon)).ToList();

            int cntr = 1;

            while (mostCommonItems.Count != 1)
            {
                mostCommonItems = DetermineO2(mostCommonItems, cntr);
                cntr++;
            }

            cntr = 1;
            while (leastCommonItems.Count != 1)
            {
                leastCommonItems = DetermineCO2(leastCommonItems, cntr);
                cntr++;
            }

            Console.WriteLine("Part 2 Solution: " + Convert.ToInt32(mostCommonItems.First(), 2) * Convert.ToInt32(leastCommonItems.First(), 2));
        }

        private List<string> DetermineO2(List<string> data, int cntr)
        {
            var nbOfresult = data.Select(fs => fs.Substring(cntr, 1)).GroupBy(item => item).OrderByDescending(g => g.Count()).Select(g => g.Count());
            var mostCommonKey = data.Select(fs => fs.Substring(cntr, 1)).GroupBy(item => item).OrderByDescending(g => g.Count()).Select( g => g.Key).First();

            if (nbOfresult.First() == nbOfresult.Last())
            {
                mostCommonKey = "1";
            }

            List<string> retValue = data.Select(x => x).Where(s => s.Substring(cntr, 1).Equals(mostCommonKey)).ToList();

            return retValue;
        }

        private List<string> DetermineCO2(List<string> data, int cntr)
        {
            var nbOfresult = data.Select(fs => fs.Substring(cntr, 1)).GroupBy(item => item).OrderByDescending(g => g.Count()).Select(g => g.Count());
            var leastCommonKey = data.Select(fs => fs.Substring(cntr, 1)).GroupBy(item => item).OrderByDescending(g => g.Count()).Select(g => g.Key).Last();

            if (nbOfresult.First() == nbOfresult.Last())
            {
                leastCommonKey = "0";
            }

            List<string> retValue = data.Select(x => x).Where(s => s.Substring(cntr, 1).Equals(leastCommonKey)).ToList();

            return retValue;
        }
    }
}
