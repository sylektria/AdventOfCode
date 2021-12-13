using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeGame
{
    public class Day9
    {
        public Day9()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day9\\Day9Input.txt");
            
            int [,] table = new int[allData.Count(), allData[0].Length];
            int[,] part2Table = new int[allData.Count(), allData[0].Length];

            for (int i = 0; i < allData.Count(); i++)
            {
                for (int j = 0; j < allData[i].Length; j++)
                {
                    table[i, j] = allData[i][j] - '0';

                    if (table[i, j] == 9)
                    {
                        part2Table[i, j] = -1;
                    }
                    else
                    {
                        part2Table[i, j] = 0;
                    }                    
                }
            }

            Console.WriteLine("Day 9:");

            Part1(table);

            Part2(part2Table);

            Console.WriteLine();
        }

        private void Part1(int[,] tableParam)
        {
            int[,] table = tableParam;
            int lowPointSum = 0;

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    List<int> least = new List<int>();

                    if ((j - 1) >= 0)
                    {
                        /* Check left */
                        if (table[i, j - 1] <= table[i, j])
                        {
                            least.Add(table[i, j - 1]);
                        }
                    }

                    if ((j + 1) < table.GetLength(1))
                    {
                        /* Check right */
                        if (table[i, j + 1] <= table[i, j])
                        {
                            least.Add(table[i, j + 1]);
                        }
                    }

                    if ((i - 1) >= 0)
                    {
                        /* Check top */
                        if (table[i - 1, j] <= table[i, j])
                        {
                            least.Add(table[i - 1, j]);
                        }
                    }

                    if ((i + 1) < table.GetLength(0))
                    {
                        /* Check Bot */
                        if (table[i + 1, j] <= table[i, j])
                        {
                            least.Add(table[i + 1, j]);
                        }
                    }

                    if (least.Count() == 0)
                    {
                        lowPointSum += (table[i, j] + 1);
                    }
                }
            }

            Console.WriteLine("Part 1 Solution: " + lowPointSum);
        }

        private void Part2(int[,] tableParam)
        {
            int[,] table = tableParam;

            List<int> basins = new List<int>();

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (table[i, j] == 0)
                    {
                        basins.Add(FindBasin(table, i, j));
                    }                    
                }
            }

            basins = basins.OrderByDescending(b => b).ToList();
            int result = basins[0] * basins[1] * basins[2];

            Console.WriteLine("Part 2 Solution: " + result);
        }

        private int FindBasin(int[,] tableParam, int startRow, int startCol)
        {
            int[,] table = tableParam;
            int cntr = 0;

            int row = table.GetLength(0);
            int col = table.GetLength(1);

            /* -1 invalid zone
             * 0  Valid zone
             * -1  Visited zone */

            // Directions
            /* right, left, up, down */
            int[,] dir = { { 0, 1 }, { 0, -1 },
                   { 1, 0 }, { -1, 0 } };

            // Queue
            Queue q = new Queue();

            // Insert the top left corner.
            q.Enqueue(new Tuple<int, int>(startRow, startCol));

            // Until queue is empty
            while (q.Count > 0)
            {
                Tuple<int, int> p = (Tuple<int, int>)(q.Peek());
                q.Dequeue();

                // Mark as visited
                table[p.Item1, p.Item2] = -2;
                
                // Check all four directions
                for (int i = 0; i < 4; i++)
                {
                    // Using the direction array
                    int a = p.Item1 + dir[i, 0];
                    int b = p.Item2 + dir[i, 1];

                    // Not blocked and valid
                    if (a >= 0 && b >= 0 &&
                       a < row && b < col &&
                          table[a, b] != -1 && table[a, b] != -2)
                    {
                        q.Enqueue(new Tuple<int, int>(a, b));
                    }
                }                
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (table[i, j] == -2)
                    {
                        table[i, j] = -1;
                        cntr++;
                    }
                }
            }

            return cntr;
        }
    }
}
