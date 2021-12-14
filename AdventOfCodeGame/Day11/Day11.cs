using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeGame
{
    public class Day11
    {
        int tableRowSize = 10;
        int tableColSize = 10;

        int flashCntr = 0;

        #region Part 2 solution
        int allFlashedCntr = 0;
        #endregion

        public Day11()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day11\\Day11Input.txt");

            int[,] octopuses = new int[tableRowSize, tableColSize];
            int[,] part2octopuses = new int[tableRowSize, tableColSize];

            for (int i = 0; i < allData.Count(); i++)
            {
                for (int j = 0; j < allData[i].Length; j++)
                {
                    octopuses[i, j] = Int32.Parse(allData[i][j].ToString());
                    part2octopuses[i, j] = Int32.Parse(allData[i][j].ToString());
                }
            }

            Console.WriteLine("Day 11:");

            Part1(octopuses);
            
            Part2(part2octopuses);

            Console.WriteLine();
        }

        private void Part1(int[,] data)
        {
            int[,] octopuses = data;
            int steps = 0;

            do
            {
                FlashOctopuses(octopuses);

                steps++;
            } while (steps != 100);

            Console.WriteLine("Part 1 Solution: " + flashCntr);

        }

        private void FlashOctopuses(int[,] octopuses)
        {
            /* top left, top, top right
             * left, right 
             * bot left, bot, bot right */
            int[,] dir = {{ -1, -1 }, {  1, 0 }, { -1, 1 },
                          {  0, -1 }, {  0, 1 },
                          {  1, -1 }, { -1,  0 }, {  1, 1 }};

            /* Increase every octopus cntr by 1 */
            for (int i = 0; i < tableRowSize; i++)
            {
                for (int j = 0; j < tableColSize; j++)
                {
                    octopuses[i, j] += 1;
                }
            }

            bool anyFlashRemains;

            do
            {
                anyFlashRemains = false;

                for (int i = 0; i < tableRowSize; i++)
                {
                    for (int j = 0; j < tableColSize; j++)
                    {
                        if (octopuses[i, j] > 9)
                        {
                            anyFlashRemains = true;
                            flashCntr++;

                            /* mark the octopus as "flashed" */
                            octopuses[i, j] = -1;

                            for (int k = 0; k < 8; k++)
                            {
                                /* Using the direction array */
                                int row = i + dir[k, 0];
                                int col = j + dir[k, 1];

                                /* Valid Position (it is inside the table) */
                                if ((row >= 0) && (col >= 0) &&
                                    (row < tableRowSize) && (col < tableColSize) && (octopuses[row, col] != -1))
                                {
                                    octopuses[row, col] += 1;
                                }
                            }
                        }
                    }
                }
            } while (anyFlashRemains);

            

            for (int i = 0; i < tableRowSize; i++)
            {
                for (int j = 0; j < tableColSize; j++)
                {
                    if (octopuses[i, j] == -1)
                    {
                        octopuses[i, j] = 0;

                        #region Part 2 solution
                        allFlashedCntr++;
                        #endregion
                    }
                }
            }
        }

        private void Part2(int[,] data)
        {
            int[,] octopuses = data;
            int steps = 1;

            do
            {
                FlashOctopuses(octopuses);

                if (allFlashedCntr == 100)
                {
                    break;
                }
                else
                {
                    allFlashedCntr = 0;
                }

                steps++;
            } while (true);

            Console.WriteLine("Part 2 solution: " + steps);
        }
    }
}
