using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeGame
{
    public class Day1
    {
        public Day1()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day1\\Day1Input.txt");

            Console.WriteLine("Day 1:");

            Part1(allData.Select(int.Parse).ToList());

            Part2(allData.Select(int.Parse).ToList());

            Console.WriteLine();
        }

        private void Part1(List<int> allData)
        {
            int counter = 0;

            for (int i = 1; i < allData.Count; i++)
            {
                if (allData[i] > allData[i - 1])
                {
                    counter++;
                }
            }

            Console.WriteLine("Part 1 Solution: " + counter);
        }

        private void Part2(List<int> allData)
        {
            int counter = 0;
            int prevSumOfThree = allData[0] + allData[1] + allData[2];

            for (int i = 1; i < allData.Count - 2; i++)
            {
                int currentSumOfThree = allData[i] + allData[i + 1] + allData[i + 2];

                if (currentSumOfThree > prevSumOfThree)
                {
                    counter++;
                }

                prevSumOfThree = currentSumOfThree;
            }

            Console.WriteLine("Part 2 Solution: " + counter);
        }
    }
}
