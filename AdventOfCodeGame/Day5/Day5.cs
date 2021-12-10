using Day5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeGame
{
    public class Day5
    {
        public Day5()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day5\\Day5Input.txt");

            List<Coordinates> HydrothermalVents = new List<Coordinates>();

            foreach (var line in allData)
            {
                List<int> result = line.Split(new string[] { "->", "," }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();
                HydrothermalVents.Add(new Coordinates(result[0], result[1], result[2], result[3]));
            }


            Console.WriteLine("Day 5:");

            Part1(HydrothermalVents);

            Part2(HydrothermalVents);

            Console.WriteLine();
        }

        private void Part1(List<Coordinates> hydrothermalVents)
        {
            int maxX1 = hydrothermalVents.Max(s => s.X1);
            int maxX2 = hydrothermalVents.Max(s => s.X2);
            int maxY1 = hydrothermalVents.Max(s => s.Y1);
            int maxY2 = hydrothermalVents.Max(s => s.Y1);

            int maxColumn = Math.Max(maxX1, maxX2) + 1;
            int maxRow = Math.Max(maxY1, maxY2) + 1;


            int[,] oceanFloor = new int[maxColumn, maxRow];

            foreach (var currentCoord in hydrothermalVents)
            {
                if ((currentCoord.X1 == currentCoord.X2) || (currentCoord.Y1 == currentCoord.Y2))
                {
                    if ((currentCoord.X1 == currentCoord.X2))
                    {
                        for (int i = Math.Min(currentCoord.Y1, currentCoord.Y2); i <= Math.Max(currentCoord.Y1, currentCoord.Y2); i++)
                        {
                            oceanFloor[currentCoord.X1, i] += 1;
                        }
                    }
                    else
                    {
                        for (int i = Math.Min(currentCoord.X1, currentCoord.X2); i <= Math.Max(currentCoord.X1, currentCoord.X2); i++)
                        {
                            oceanFloor[i, currentCoord.Y1] += 1;
                        }
                    }
                }
            }

            var dangeorusTiles = (from int item in oceanFloor
                        where item > 1
                        select item).Count();

            Console.WriteLine("Part 1 Solution: " + dangeorusTiles);

            /* Print table for test */
            /*for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxColumn; j++)
                {
                    Console.Write(string.Format("{0} ", oceanFloor[j, i]));
                }
                Console.Write(Environment.NewLine);
            }*/
        }


        private void Part2(List<Coordinates> hydrothermalVents)
        {
            int maxX1 = hydrothermalVents.Max(s => s.X1);
            int maxX2 = hydrothermalVents.Max(s => s.X2);
            int maxY1 = hydrothermalVents.Max(s => s.Y1);
            int maxY2 = hydrothermalVents.Max(s => s.Y1);

            int maxColumn = Math.Max(maxX1, maxX2) + 1;
            int maxRow = Math.Max(maxY1, maxY2) + 1;


            int[,] oceanFloor = new int[maxColumn, maxRow];

            foreach (var currentCoord in hydrothermalVents)
            {
                /* if 2 coordinate points match. 1,5 -> 8,5  or 8,9 -> 8,0*/
                if ((currentCoord.X1 == currentCoord.X2) || (currentCoord.Y1 == currentCoord.Y2))
                {
                    if ((currentCoord.X1 == currentCoord.X2))
                    {
                        for (int i = Math.Min(currentCoord.Y1, currentCoord.Y2); i <= Math.Max(currentCoord.Y1, currentCoord.Y2); i++)
                        {
                            oceanFloor[currentCoord.X1, i] += 1;
                        }
                    }
                    else
                    {
                        for (int i = Math.Min(currentCoord.X1, currentCoord.X2); i <= Math.Max(currentCoord.X1, currentCoord.X2); i++)
                        {
                            oceanFloor[i, currentCoord.Y1] += 1;
                        }
                    }
                }
                /* Diagonal line, both coordinates have the same value. 1,1 -> 3,3 */
                else if ((currentCoord.X1 == currentCoord.Y1) && (currentCoord.X2 == currentCoord.Y2))
                {
                    for (int i = Math.Min(currentCoord.X1, currentCoord.X2); i <= Math.Max(currentCoord.X1, currentCoord.X2); i++)
                    {
                        oceanFloor[i, i] += 1;
                    }
                }
                /* Diagonal line, x1 coordinates have the same value as y2, and x2 with y1. 9,7 -> 7,9 */
                else if ((currentCoord.X1 == currentCoord.Y2) && (currentCoord.X2 == currentCoord.Y1))
                {
                    int maxValue = Math.Max(currentCoord.X1, currentCoord.Y1);
                    int minValue = Math.Min(currentCoord.X1, currentCoord.Y1);

                    for (int i = 0; i <= maxValue-minValue; i++)
                    {
                        oceanFloor[i + minValue, maxValue - i] += 1;
                    }
                }
                else
                {
                    int maxCycleElement = Math.Max(currentCoord.X1, currentCoord.X2) - Math.Min(currentCoord.X1, currentCoord.X2);

                    /* Check diagonal direction */
                    /* If both points is greater or equal, direction is bot right to top left  */
                    if (((currentCoord.X1 >= currentCoord.X2) && (currentCoord.Y1 >= currentCoord.Y2)) ||
                        ((currentCoord.X2 >= currentCoord.X1) && (currentCoord.Y2 >= currentCoord.Y1)))
                    {
                        for (int i = 0; i <= maxCycleElement; i++)
                        {
                            oceanFloor[Math.Min(currentCoord.X1, currentCoord.X2) + i, Math.Min(currentCoord.Y1, currentCoord.Y2) + i] += 1;
                        }
                    }
                    /* Otherwise is bot left to top right */
                    else
                    {
                        for (int i = 0; i <= maxCycleElement; i++)
                        {
                            oceanFloor[Math.Min(currentCoord.X1, currentCoord.X2) + i, Math.Max(currentCoord.Y1, currentCoord.Y2) - i] += 1;
                        }
                    }                    
                }
            }

            var dangeorusTiles = (from int item in oceanFloor
                                  where item > 1
                                  select item).Count();

            Console.WriteLine("Part 2 Solution: " + dangeorusTiles);

            /* Print table for test */
            /*for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxColumn; j++)
                {
                    Console.Write(string.Format("{0} ", oceanFloor[j, i]));
                }
                Console.Write(Environment.NewLine);
            }*/
        }
    }
}
