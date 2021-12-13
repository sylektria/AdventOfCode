using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeGame
{
    public class Day2
    {
        public Day2()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day2\\Day2Input.txt");

            Console.WriteLine("Day 2:");

            Part1(allData);

            Part2(allData);

            Console.WriteLine();
        }

        private void Part1(List<string> allData)
        {
            int horizontalPos = 0;
            int depth = 0;

            foreach (string data in allData)
            {
                List<string> SplitData = data.Split(' ').ToList();

                switch (SplitData[0].ToString().ToLower())
                {
                    case "forward":
                        horizontalPos += Int32.Parse(SplitData[1]);
                        break;

                    case "up":
                        depth -= Int32.Parse(SplitData[1]);
                        break;

                    case "down":
                        depth += Int32.Parse(SplitData[1]);
                        break;

                    default:
                        break;
                }
            }

            Console.WriteLine("Part 1 Solution: " + (horizontalPos * depth));
        }

        private void Part2(List<string> allData)
        {
            int horizontalPos = 0;
            int depth = 0;
            int aim = 0;

            foreach (string data in allData)
            {
                List<string> SplitData = data.Split(' ').ToList();

                switch (SplitData[0].ToString().ToLower())
                {
                    case "forward":
                        horizontalPos += Int32.Parse(SplitData[1]);
                        depth += aim * Int32.Parse(SplitData[1]);
                        break;

                    case "up":
                        aim -= Int32.Parse(SplitData[1]);
                        break;

                    case "down":
                        aim += Int32.Parse(SplitData[1]);
                        break;

                    default:
                        break;
                }
            }

            Console.WriteLine("Part 2 Solution: " + (horizontalPos * depth));
        }

    }
}
