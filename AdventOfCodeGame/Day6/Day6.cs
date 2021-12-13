using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeGame
{
    public class Day6
    {
        public Day6()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day6\\Day6Input.txt");

            List<int> lanternfishes = new List<int>();

            lanternfishes = allData[0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();

            Console.WriteLine("Day 6:");

            Part1(lanternfishes);

            Part2(lanternfishes);

            Console.WriteLine();
        }

        private void Part1(List<int> lanternfishes)
        {
            Console.WriteLine("Part 1 Solution: " + CountTheLanternfishes(lanternfishes, 80, false));
        }

        private long CountTheLanternfishes(List<int> lanternfishes, int NbOfDays, bool writeSubCounting)
        {
            int dayCounter = 0;

            long[] lanterfishList = new long[9];

            foreach (var item in lanternfishes)
            {
                lanterfishList[item]++;
            }

            if (writeSubCounting)
            {
                Console.Write("Initial List: ");

                foreach (var item in lanterfishList)
                {
                    Console.Write(item + ",");
                }

                Console.WriteLine();
            }            

            do
            {
                long[] newPopulation = new long[9];


                for (int currentFish = 7; currentFish >= 0; currentFish--)
                {
                    if (lanterfishList[currentFish + 1] != 0)
                    {
                        newPopulation[currentFish] = lanterfishList[currentFish + 1];
                    }
                    if (currentFish == 0)
                    {
                        newPopulation[8] = lanterfishList[0];
                        newPopulation[6] += lanterfishList[0];
                        newPopulation[currentFish] = lanterfishList[currentFish + 1];
                    }
                }

                dayCounter++;

                lanterfishList = newPopulation;

                if (writeSubCounting)
                {
                    Console.Write("After " + dayCounter + " Day: ");

                    foreach (var item in lanterfishList)
                    {
                        Console.Write(item + ",");
                    }

                    Console.WriteLine();
                }

            } while (dayCounter != NbOfDays);

            return lanterfishList.Sum();            
        }

        private void Part2(List<int> lanternfishes)
        {
            Console.WriteLine("Part 2 Solution: " + CountTheLanternfishes(lanternfishes, 256, false));
        }
    }
}
