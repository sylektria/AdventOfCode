using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeGame
{
    public class Day4
    {
        public Day4()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day4\\Day4Input.txt");

            /* first row contains the drawn Bingo numbers */
            List<int> BingoNumbers = allData[0].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();
            List<int[,]> BingoTables = new List<int[,]>();

            Console.WriteLine("Day 4:");

            SaveTables(allData, ref BingoTables);

            Part1(BingoNumbers, BingoTables);

            Part2(BingoNumbers, BingoTables);

            Console.WriteLine();
        }


        private void SaveTables(List<string> allData, ref List<int[,]> bingoTables)
        {
            int[,] tmpTable = new int[5, 5];

            for (int i = 2, column = 0, row = 0; i < allData.Count; i++)
            {
                List<int> tmplist = new List<int>();
                tmplist = allData[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToList();

                if (tmplist.Count != 0)
                {
                    foreach (var item in tmplist)
                    {
                        tmpTable[row, column] = item;
                        column++;
                    }
                    column = 0;
                    row++;
                }
                else
                {
                    bingoTables.Add(tmpTable);
                    tmpTable = new int[5, 5];
                    column = 0;
                    row = 0;
                }
            }
        }

        private void Part1(List<int> bingoNumbers, List<int[,]> bingoTables)
        {
            List<int[,]> tables = bingoTables;
            int column = 0, row = 0;

            foreach (var number in bingoNumbers)
            {
                for (int tableIdx = 0; tableIdx < tables.Count; tableIdx++)
                {
                    row = 0;

                    for (int i = 0; i < tables[tableIdx].Length; i++)
                    {
                        if (tables[tableIdx][row, column] == number)
                        {
                            tables[tableIdx][row, column] = -1;
                        }

                        column++;

                        if (((i + 1) % 5) == 0)
                        {
                            row++;
                            column = 0;
                        }
                    }

                    if (CheckBingo(tables[tableIdx]))
                    {
                        int sum = 0;

                        foreach (var item in tables[tableIdx])
                        {
                            if (!item.Equals(-1))
                            {
                                sum += item;
                            }
                        }

                        sum *= number;

                        Console.WriteLine("Part 1 Solution: " + "index: " + tableIdx + " " + sum);
                        return;
                    }
                }
            }
        }


        private bool CheckBingo(int[,] table)
        {
            int rowCntr = 0;
            int columnCntr = 0;

            for (int row = 0; row < 5; row++)
            {
                for (int column = 0; column < 5; column++)
                {
                    if (table[row, column] == -1)
                    {
                        rowCntr++;
                    }

                    if (table[column, row] == -1)
                    {
                        columnCntr++;
                    }

                }

                if (rowCntr == 5 || columnCntr == 5)
                {
                    return true;
                }

                columnCntr = 0;
                rowCntr = 0;
            }

            return false;
        }

        private void Part2(List<int> bingoNumbers, List<int[,]> bingoTables)
        {
            List<int[,]> tables = bingoTables;
            int column = 0, row = 0;

            List<int> allTableIdx = new List<int>();

            int lastNumber = 0;
            bool lastBingo = false;

            foreach (var number in bingoNumbers)
            {
                if ((tables.Count == 1) && (lastBingo == true))
                {
                    int sum = 0;

                    foreach (var item in tables[0])
                    {
                        if (!item.Equals(-1))
                        {
                            sum += item;
                        }
                    }

                    sum *= lastNumber;

                    Console.WriteLine("Part 2 Solution: " + sum);
                    return;
                }
                else
                {
                    List<int> tmpRemovedIdx = new List<int>();

                    for (int tableIdx = 0; tableIdx < tables.Count; tableIdx++)
                    {
                        row = 0;

                        for (int i = 0; i < tables[tableIdx].Length; i++)
                        {
                            if (tables[tableIdx][row, column] == number)
                            {
                                tables[tableIdx][row, column] = -1;
                            }

                            column++;

                            if (((i + 1) % 5) == 0)
                            {
                                row++;
                                column = 0;
                            }
                        }

                        if (CheckBingo(tables[tableIdx]))
                        {
                            tmpRemovedIdx.Add(tableIdx);
                        }
                    }

                    if ((tables.Count == 1) && (tmpRemovedIdx.Count == 1))
                    {
                        lastBingo = true;
                        lastNumber = number;
                    }
                    else
                    {
                        foreach (var tableIndex in tmpRemovedIdx.OrderByDescending(i => i))
                        {
                            tables.RemoveAt(tableIndex);
                        }
                    }                    
                }
            }
        }
    }
}
