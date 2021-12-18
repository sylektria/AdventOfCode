using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeGame
{
    public class Paper
    {
        private int[,] Table { get; set; }

        private int maxCol, maxRow;

        public Paper(int col, int row)
        {
            Table = new int[col,row];
            maxCol = col;
            maxRow = row;
        }

        /// <summary>
        /// Sets the initial points in the table
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        public void SetStartingPosition(int col, int row)
        {
            Table[col, row] += 1;
        }


        /// <summary>
        /// Prints all the table element
        /// </summary>
        public void PrintTable()
        {
            for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxCol; j++)
                {
                    if (Table[j, i] == 0)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write("#");
                    }
                    //Console.Write(string.Format("{0} ", Table[j, i]));
                }

                Console.Write(Environment.NewLine);
            }
        }

        /// <summary>
        /// Folds the paper based on the input.
        /// </summary>
        /// <param name="horizontalOrVertical"></param>
        /// <param name="pos"></param>
        public void FoldPaper(string horizontalOrVertical, int pos)
        {
            /* bot paper to up */
            if (horizontalOrVertical.Equals("y"))
            {
                int[,] newTable = GetReducedCopyTable(0, 0, maxCol, pos);
                int[,] oldFoldedPointsTable = GetReducedCopyTable(0, pos + 1, maxCol, maxRow);

                int cntr = 0;
                /* new table row */
                int i = pos - 1;

                /* folded table row */
                int l = 0;

                do
                {
                    for (int j = 0; j < maxCol; j++)
                    {
                        newTable[j, i] += oldFoldedPointsTable[j, l];
                    }

                    i--;
                    l++;
                    cntr++;
                } while (cntr != (maxRow - pos - 1));

                maxRow = pos;

                Table = newTable;
            }
            /* right paper to left */
            else
            {
                int[,] newTable = GetReducedCopyTable(0, 0, pos, maxRow);
                int[,] oldFoldedPointsTable = GetReducedCopyTable(pos + 1, 0, maxCol, maxRow);

                int cntr = 0;
                /* new table column */
                int j = pos - 1;

                /* folded table column */
                int k = 0;

                do
                {
                    for (int i = 0; i < maxRow; i++)
                    {
                        newTable[j, i] += oldFoldedPointsTable[k, i];
                    }

                    j--;
                    k++;
                    cntr++;
                } while (cntr != (maxCol - pos - 1));

                maxCol = pos;

                Table = newTable;
            }
        }

        /// <summary>
        /// returns the number of points in the table where the coordinate value is higher than 0.
        /// </summary>
        /// <returns></returns>
        public int GetDotsOnPaper()
        {
            int cntr = 0;

            for (int i = 0; i < maxRow; i++)
            {
                for (int j = 0; j < maxCol; j++)
                {
                    if (Table[j, i] > 0)
                    {
                        cntr++;
                    }
                }
            }

            return cntr;
        }

        /// <summary>
        /// Get and return the original table points from the given parameters.
        /// ex.: table is 15x15, returns the fragment of the table based on the parameters.
        /// </summary>
        /// <param name="startCol"></param>
        /// <param name="startRow"></param>
        /// <param name="maxCol"></param>
        /// <param name="maxRow"></param>
        /// <returns></returns>
        private int[,] GetReducedCopyTable(int startCol, int startRow, int maxCol, int maxRow)
        {
            int[,] newTable = new int[maxCol - startCol, maxRow - startRow];

            /* Copy existing points */
            for (int i = startRow; i < maxRow; i++)
            {
                for (int j = startCol; j < maxCol; j++)
                {
                    newTable[j - startCol, i - startRow] = Table[j, i];
                }
            }

            return newTable;
        }

    }
}
