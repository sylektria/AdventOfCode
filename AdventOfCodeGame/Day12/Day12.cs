using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeGame
{
    public class Day12
    {
        /// <summary>
        /// Check if the string is all upper or not
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsUpper(input[i]))
                    return false;
            }

            return true;
        }

        public Day12()
        {
            TextReader txtreader = new TextReader();

            List<string> allData = new List<string>();

            allData = txtreader.ReadText("Day12\\Day12Input.txt");

            Console.WriteLine("Day 12:");

            Dictionary<string, int> GraphPoints = new Dictionary<string, int>();

            GraphPoints.Add("start", 0);
            GraphPoints.Add("end", 1);

            int uniqueValue = 2;

            /* fill the dictionary */
            foreach (var line in allData)
            {
                List<string> tmpList = line.Split('-').ToList();

                if (GraphPoints.ContainsKey(tmpList[0]) != true)
                {
                    GraphPoints.Add(tmpList[0], uniqueValue);
                    uniqueValue++;
                }

                if (GraphPoints.ContainsKey(tmpList[1]) != true)
                {
                    GraphPoints.Add(tmpList[1], uniqueValue);
                    uniqueValue++;
                }
            }

            /* index of all small cave without start and end */
            List<int> smallCaveIdx = new List<int>();

            foreach (var item in GraphPoints)
            {
                if ((!IsAllUpper(item.Key)) && item.Key != "start" && item.Key != "end")
                {
                    smallCaveIdx.Add(item.Value);
                }
            }

            /* Graph Start point */
            int s = 0;

            /* Graph End point */
            int d = 1;

            Part1(s, d, GraphPoints, allData, smallCaveIdx);

            Part2(s, d, GraphPoints, allData);

            Console.WriteLine();
        }

        private void Part1(int startPos, int endPos, Dictionary<string, int> graphPoints, List<string> allData, List<int> smallcaveidx)
        {
            Graph graph = new Graph(graphPoints.Count(), graphPoints, smallcaveidx);

            /* Add edges */
            foreach (var line in allData)
            {
                List<string> tmpList = line.Split('-').ToList();

                graph.AddEdge(graphPoints[tmpList[0]], graphPoints[tmpList[1]]);
                graph.AddEdge(graphPoints[tmpList[1]], graphPoints[tmpList[0]]);
            }

            /*Console.WriteLine("Following are all different"
                  + " paths from " + startPos + " to " + endPos); */

            bool calledFromPart1 = true;
            graph.PrintAllPaths(startPos, endPos, calledFromPart1);

            Console.WriteLine("Part 1 Solution: " + graph.NbOfUniquePath);
        }

        private void Part2(int startPos, int endPos, Dictionary<string, int> graphPoints, List<string> allData)
        {

        }
    }
}
